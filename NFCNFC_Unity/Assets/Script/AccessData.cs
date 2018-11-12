using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccessData : MonoBehaviour
{
    public InputField input;
    public string extract_username;
    public string extract_password;
    public string extract_mobile;

    public Text Register_warntext = null;
    public Text title;
    public GameObject Login_button;
    public GameObject Back_to_Reg_button;
    public GameObject Reg_panel;	
	public static string Server = "etufo.000webhostapp.com";
    public string CreateUserURL = Server + "/registration.php";

    public void Awaking()
    {


    }
    // Use this for initialization
    void Start()
    {
        
    }
		
    public void CreateUser()
    {
        input = GameObject.Find("In_Username").GetComponent<InputField>();
        extract_username = input.text;

        input = GameObject.Find("In_Password").GetComponent<InputField>();
        extract_password = input.text;

        input = GameObject.Find("In_Mobile").GetComponent<InputField>();
        extract_mobile = input.text;

        StartCoroutine(FeedbackFromSite(extract_username, extract_password, extract_mobile));

    }

    IEnumerator FeedbackFromSite(string user_username, string user_password, string user_mobile)
    {
        WWWForm form = new WWWForm();
        form.AddField("penggunaid", extract_username);
        form.AddField("katalaluan", extract_password);
        form.AddField("mobilenum", extract_mobile);
        
        WWW www = new WWW("https://" + CreateUserURL, form);

        yield return www;
        Debug.Log(www.text);

        if (www.text == "Duplicate")
        {
            Reg_panel.SetActive(true);
            Reg_panel.transform.GetChild(0).gameObject.SetActive(true);
            Reg_panel.transform.GetChild(1).gameObject.SetActive(true);
            Register_warntext.text = "Registration Failed. This phone number might be used.";
        }

        else if (www.text == "Verify")
        {
            Reg_panel.SetActive(true);
            Reg_panel.transform.GetChild(0).gameObject.SetActive(true);
            Reg_panel.transform.GetChild(2).gameObject.SetActive(true);
            Register_warntext.text = "Registration succesful. Please check your email for account activation.";
        }

        else if (www.text == "Everything OK")
        {
            Reg_panel.SetActive(true);
            Reg_panel.transform.GetChild(0).gameObject.SetActive(true);
            Reg_panel.transform.GetChild(2).gameObject.SetActive(true);
            Register_warntext.text = "Registration succesful. ";
        }

        else if (www.text == "Incomplete Data")
        {
            Reg_panel.SetActive(true);
            Reg_panel.transform.GetChild(0).gameObject.SetActive(true);
            Reg_panel.transform.GetChild(1).gameObject.SetActive(true);
            Register_warntext.text = "Incomplete data.";
        }
    }
}
