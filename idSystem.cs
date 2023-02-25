public class IDCard : ScriptableObject
{
    public string firstName;
    public string lastName;
    public string birthDate;
    public string idNumber;
    public Genders gender;
    public Sprite photo;
    public Sprite signature;
}

public class DynamicTextController : MonoBehaviour
{
    public TextMeshProUGUI firstName;
    public TextMeshProUGUI lastName;
    public TextMeshProUGUI birthDate;
    public TextMeshProUGUI idNumber;
    public TextMeshProUGUI gender;
    public Image photo;
    public Image signature;
}

public class VariableNameComparer : MonoBehaviour
{
    public IDCard idCardData;
    public DynamicTextController textController;

    private void Start()
    {
        // Get the list of fields in the IDCard class
        FieldInfo[] idCardFields = idCardData.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

        // Get the list of fields in the DynamicTextController class
        FieldInfo[] textControllerFields = textController.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

        // Loop through each field in the DynamicTextController class and check if there is a matching field in the IDCard class
        foreach (FieldInfo textControllerField in textControllerFields)
        {
            foreach (FieldInfo idCardField in idCardFields)
            {
                if (textControllerField.Name.ToLower() == idCardField.Name.ToLower())
                {
                    // Found a matching field, check if it is of type TextMeshProUGUI
                    if (textControllerField.FieldType == typeof(TextMeshProUGUI))
                    {
                        // Get the TextMeshProUGUI component from the DynamicTextController field
                        TextMeshProUGUI textMeshProUGUI = (TextMeshProUGUI)textControllerField.GetValue(textController);

                        // Get the value of the matching field from the IDCard class
                        object fieldValue = idCardField.GetValue(idCardData);

                        // Set the text property of the TextMeshProUGUI component to the value of the matching field
                        textMeshProUGUI.text = fieldValue.ToString();
                    }
                }
            }
        }
    }
}
