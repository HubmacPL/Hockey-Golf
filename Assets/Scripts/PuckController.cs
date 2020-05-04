using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckController : MonoBehaviour
{
    //Movement setting
    [Header("Movement setting")]
    public float forceRatio = 1.0f;
    public float maxForce = 10.0f;

    //About rotation and camera
    [Header("Abaut rotation and camera")]
    public float cameraRotationSpeed = 2.0f;
    public float cameraRotationAcceleration = 1f;
    public float aimRotationSpeed = 5.0f;
    public GameObject cameraHandle;
    [Header("Another values")]

    //Shooting varables
    public PuckSound puckSound;
    private Rigidbody rigidbody;
    private GameObject aimSupporter;
    private bool shootMode = false;
    public float force;

    //Layers
    private int rfLayerMask;
    private int puckLayerMask;

    //Line renderer
    private LineRenderer lineRenderer;

    //Events

    public delegate void OnEventShoot();

    public event OnEventShoot ShootsEvents;

    // Start is called before the first frame update
    void Start()
    {
        rfLayerMask = LayerMask.GetMask("RayFloor");
        puckLayerMask = LayerMask.GetMask("Puck");
        rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.centerOfMass = Vector3.zero;
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        OnClickPuck();
        Shooting();
        //RotateToMoveDirection();
    }
    private void Shooting()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction, Color.yellow);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, rfLayerMask))
        {
            if (Input.GetKeyUp(KeyCode.Mouse0) && shootMode == true)    //Shoot moment
            {
                shootMode = false;
                Destroy(aimSupporter);

                Vector3 shootDirection = aimSupporter.GetComponent<AngleTest>().direction;
                force = Vector3.Distance(aimSupporter.transform.position, transform.position) * forceRatio;
                force = Mathf.Clamp(force, 0, maxForce);

                rigidbody.AddForce(shootDirection * force, ForceMode.Force);
                puckSound.ShotSound(force/maxForce);

                lineRenderer.positionCount = 0;

                RaiseShootEvent();
            }
            else if (shootMode == true && Input.GetKey(KeyCode.Mouse0))     //Aiming
            {
                aimSupporter.transform.position = hit.point;
                AngleTest angleTest = aimSupporter.GetComponent<AngleTest>();
                angleTest.rotationSpeed = aimRotationSpeed * Vector3.Distance(aimSupporter.transform.position, transform.position);

                /*      OLD AIMING SYSTEM
                Quaternion lookRotation = Quaternion.LookRotation(angleTest.direction);

                Vector3 lookRotationVector = lookRotation.eulerAngles;
                lookRotationVector.y = lookRotationVector.y -90;

                lookRotation = Quaternion.Euler(lookRotationVector);

                float rotSpd = aimRotationSpeed * Vector3.Distance(transform.position, aimSupporter.transform.position);

                cameraHandle.transform.rotation = Quaternion.Slerp(cameraHandle.transform.rotation, lookRotation, Time.deltaTime * rotSpd);
                */

                Vector3[] linePoints = new Vector3[2];
                linePoints[0] = aimSupporter.transform.position;
                linePoints[1] = transform.position;
                DrawLine(linePoints);
            }
        }
    }

    private void OnClickPuck()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Input.GetKeyDown(KeyCode.Mouse0) && rigidbody.velocity == Vector3.zero)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, puckLayerMask))
            {
                if (hit.collider.isTrigger)
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        ShootInit();
                    }
                }
            }
        }
    }

    private void ShootInit()
    {
        shootMode = true;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, rfLayerMask))
        {
            GameObject gm = new GameObject("AimSuporter");
            gm.AddComponent<AngleTest>();

            aimSupporter = Instantiate(gm, hit.point, Quaternion.identity);

            AngleTest gmAngleTest = aimSupporter.GetComponent<AngleTest>();

            gmAngleTest.target = gameObject.transform;
            gmAngleTest.rotationSpeed = aimRotationSpeed * Vector3.Distance(aimSupporter.transform.position, transform.position);

            Destroy(gm);
        }
    }
    private void RotateToMoveDirection()
    {
        Vector3 rbVelocity = rigidbody.velocity;
        rbVelocity.y = 0;

        bool vectorIsZero = false;

        if(ToZeroValue(rbVelocity.x) < 0.3f)
        {
            if(ToZeroValue(rbVelocity.z) < 0.3f)
            {
                vectorIsZero = true;
            }
        }

        float xSpeed = ToZeroValue(rbVelocity.x);
        float zSpeed = ToZeroValue(rbVelocity.z);
        float fastedSpeed;

        if(xSpeed >= zSpeed)
        {
            fastedSpeed = xSpeed;
        }
        else
        {
            fastedSpeed = zSpeed;
        }

        

        if(vectorIsZero == false && shootMode == false)
        {
            Quaternion lookRotation = Quaternion.LookRotation(rbVelocity);

            lookRotation = Quaternion.Euler(lookRotation.eulerAngles.x, lookRotation.eulerAngles.y-90, lookRotation.eulerAngles.z);

            float rotationAccelerationRatio = fastedSpeed / cameraRotationAcceleration;

            cameraHandle.transform.localRotation = Quaternion.Slerp(cameraHandle.transform.localRotation, lookRotation, (Time.deltaTime * cameraRotationSpeed) / rotationAccelerationRatio);

        }
    }
    private void DrawLine(Vector3[] vectors)
    {
        lineRenderer.positionCount = vectors.Length;
        lineRenderer.SetPositions(vectors);
    }
    private float ToZeroValue(float f)
    {
        if(f==0)
        {
            return 0.0f;
        }
        else if(f > 0)
        {
            return f;
        }
        else
        {
            return Mathf.Sqrt(f * f);
        }
    }
    public void RaiseShootEvent()
    {
        if(ShootsEvents != null)
        {
            ShootsEvents();
        }
        else
        {
            Debug.Log("There are not events in ShootEvent");
        }
    }
}
