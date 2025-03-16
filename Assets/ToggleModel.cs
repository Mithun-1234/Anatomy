using UnityEngine;

public class ToggleModel : MonoBehaviour
{
    // Main body models
    public GameObject Human;
    public GameObject MuscularSystem;
    public GameObject Skeleton;
    public GameObject Veins;
    public GameObject Nerves;
    public GameObject Digestive;

    // Organ models
    public GameObject Heart;
    public GameObject Brain;
    public GameObject Lungs;
    public GameObject Kidney;

    // Canvases
    public GameObject organSelectionCanvas; // Yes/No selection canvas
    public GameObject OrganCanvas; // Organ selection canvas

    // Back Button
    public GameObject BackButton; // Assign in Unity Inspector

    private int currentModel = 0;
    private int totalModels = 6;
    private bool isCanvasActive = false; // Track if canvas is active
    private GameObject activeOrgan = null; // Track active organ model

    void Start()
    {
        HideAllOrgans(); // Ensure organs are hidden initially
        ShowModel(0); // Show Human Model initially
        organSelectionCanvas.SetActive(false); // Hide first canvas
        OrganCanvas.SetActive(false); // Hide organ selection canvas
        BackButton.SetActive(false); // Hide back button initially
    }

    public void OnTargetFound()
    {
        HideAllOrgans();
        ShowModel(0); // Show human model when the target is found
        organSelectionCanvas.SetActive(false);
        OrganCanvas.SetActive(false);
        BackButton.SetActive(false); // Make sure back button is hidden initially
    }

    public void OnTargetLost()
    {
        HideAllModels();
        HideAllOrgans();
    }

    void Update()
    {
        if (isCanvasActive) return; // Prevent switching when canvas is active

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject == Digestive)
                {
                    ShowOrganSelectionCanvas();
                }
                else if (hit.transform.gameObject == Human ||
                         hit.transform.gameObject == MuscularSystem ||
                         hit.transform.gameObject == Skeleton ||
                         hit.transform.gameObject == Veins ||
                         hit.transform.gameObject == Nerves)
                {
                    ToggleVisibility();
                }
            }
        }
    }

    void ShowOrganSelectionCanvas()
    {
        HideAllModels();
        organSelectionCanvas.SetActive(true);
        isCanvasActive = true;
        BackButton.SetActive(false); // Ensure back button is hidden when showing the selection canvas
    }

    void ToggleVisibility()
    {
        currentModel = (currentModel + 1) % totalModels;
        ShowModel(currentModel);
    }

    void ShowModel(int index)
    {
        HideAllModels();
        HideAllOrgans();

        if (index == 0) Human.SetActive(true);
        else if (index == 1) MuscularSystem.SetActive(true);
        else if (index == 2) Skeleton.SetActive(true);
        else if (index == 3) Veins.SetActive(true);
        else if (index == 4) Nerves.SetActive(true);
        else if (index == 5) Digestive.SetActive(true);
    }

    void HideAllModels()
    {
        Human.SetActive(false);
        MuscularSystem.SetActive(false);
        Skeleton.SetActive(false);
        Veins.SetActive(false);
        Nerves.SetActive(false);
        Digestive.SetActive(false);
    }

    public void OnYesButtonClick()
    {
        organSelectionCanvas.SetActive(false);
        OrganCanvas.SetActive(true);
        isCanvasActive = false;
        BackButton.SetActive(false); // Hide back button since user is selecting an organ
    }

    public void OnNoButtonClick()
    {
        organSelectionCanvas.SetActive(false);
        ShowModel(0);
        isCanvasActive = false;
        BackButton.SetActive(false);
    }

    // ORGAN SELECTION FUNCTIONS
    void HideAllOrgans()
    {
        Heart.SetActive(false);
        Brain.SetActive(false);
        Lungs.SetActive(false);
        Kidney.SetActive(false);
    }

    public void ShowHeart()
    {
        ShowOrgan(Heart);
    }

    public void ShowBrain()
    {
        ShowOrgan(Brain);
    }

    public void ShowLungs()
    {
        ShowOrgan(Lungs);
    }

    public void ShowKidney()
    {
        ShowOrgan(Kidney);
    }

    private void ShowOrgan(GameObject organ)
    {
        HideAllModels();
        HideAllOrgans();

        organ.SetActive(true);
        activeOrgan = organ;

        OrganCanvas.SetActive(false);
        BackButton.SetActive(true);
    }

    // BACK BUTTON FUNCTION
    public void GoBack()
    {
        if (activeOrgan != null)
        {
            activeOrgan.SetActive(false); // Hide current organ
            activeOrgan = null;
        }

        OrganCanvas.SetActive(true); // Show back the organ selection canvas
        BackButton.SetActive(false); // Hide back button
    }
}
