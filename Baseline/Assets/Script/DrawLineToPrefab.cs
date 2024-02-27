using UnityEngine;
using iiscommon.Utilities;

public class DrawLineToPrefab : MonoBehaviour
{
    public GameObject dotPrefab;
    public GameObject drawLineButton; // Add this line
    private GameObject dotInstance;
    private LineRenderer lineRenderer;
    private bool isToggledOn = false;
    private DotManager dotManager;
    private Vector3 previousPosition;

    private void Start()
    {
        dotManager = FindObjectOfType<DotManager>();
    }

    public void OnButtonClick()
    {
        isToggledOn = !isToggledOn;

        if (isToggledOn)
        {
            if (dotInstance == null)
            {
                dotInstance = Instantiate(dotPrefab);
                dotInstance.name = "DotInstance";
                dotInstance.transform.parent = drawLineButton.transform.parent.parent; // Modified line
                dotInstance.transform.localPosition = new Vector3(-0.005025707f, -3.98792e-06f, -0.0005170768f);

                // Create the LineRenderer on the dotInstance
                lineRenderer = dotInstance.AddComponent<LineRenderer>();
                lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
                lineRenderer.startColor = lineRenderer.endColor = Color.white;
                lineRenderer.startWidth = lineRenderer.endWidth = 0.003f;
                lineRenderer.positionCount = 2;
                lineRenderer.enabled = true;

                dotManager.RegisterDot(dotInstance);
                previousPosition = dotInstance.transform.position;
            }
        }
        else
        {
            if (dotInstance != null)
            {
                dotManager.UnregisterDot(dotInstance);
                Destroy(dotInstance);
                dotInstance = null;
                lineRenderer = null;
            }
        }
    }

    private void Update()
    {
        if (dotInstance != null)
        {
            if (dotInstance.transform.position != previousPosition)
            {
                //dotManager.LogMessage(dotInstance.name + " moved");
                previousPosition = dotInstance.transform.position;
            }

            lineRenderer.SetPosition(0, this.transform.position); // Modify this line
            lineRenderer.SetPosition(1, dotInstance.transform.position);
        }
    }
}
