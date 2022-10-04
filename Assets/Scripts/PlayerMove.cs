using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private LayerMask UILayer;
    [SerializeField] private Gun[] guns;

    private SpaceShipControl inputActions;
    private bool isDrag = false;
    [HideInInspector] public bool isGameStarted = false;
    [HideInInspector] public Spawner spawner;


    [SerializeField]  private GraphicRaycaster graphicRaycast;

    private void Awake()
    {
        inputActions = new SpaceShipControl();
    }
    private void Start()
    {
        inputActions.MoveAndShoot.StartDrag.performed += _ => StartDrag();
        inputActions.MoveAndShoot.StartDrag.canceled += _ => EndDrag();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        if(isGameStarted)
            Drag();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out Enemy enemy))
        {
            Lose();
            GameObject go = Instantiate(new GameObject());

            Bullet b = go.AddComponent<Bullet>();
            enemy.SetHealth(0);
            b.SetDamage(1);
            enemy.TakeDamage(b);
        }
    }

    private void Lose()
    {
        spawner.LoseGame();
    }

    private void GetUIElementsClicked()
    {
        PointerEventData clickedData = new PointerEventData(EventSystem.current);
        clickedData.position = inputActions.MoveAndShoot.Position.ReadValue<Vector2>();

        List<RaycastResult> raycastResults = new List<RaycastResult>();

        graphicRaycast.Raycast(clickedData, raycastResults);

        if(raycastResults.Count > 0)
        {
            return;
        }
        else
        {
            spawner.StartGame();
            isGameStarted = true;
        }

    }

    public void StartDrag()
    {
        GetUIElementsClicked();
        if (isGameStarted)
        {
            isDrag = true;
            Time.timeScale = 1;
            foreach(Gun g in guns)
            {
                g.StartShoot();
            }

            if (!isGameStarted)
            {
                spawner.StartGame();
            }
        }
    }

    private void Drag()
    {
        if (!isDrag) return;

        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(inputActions.MoveAndShoot.Position.ReadValue<Vector2>());

        transform.position = Vector2.MoveTowards(transform.position, clickPosition, 5f * Time.deltaTime);
    }
    
    private void EndDrag()
    {
        isDrag = false;
        Time.timeScale = 0.2f;
        foreach (Gun g in guns)
        {
            g.StopShoot();
            
        }
    }
}
