﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using MaterialUI;
using UnityEngine.EventSystems;

public class PatientProfileManager : MonoBehaviour {

    //Patient Profile outputs
    public Text id_textbox;
    public Text fullname_textbox;
    public Text gender_textbox;
    public Text birthdate_textbox;
    public Text weight_textbox;
    public Text height_textbox;
    public Text phone_textbox;
    public Text appointment_textbox;
    public Text latest_visit_date_textbox;
    public Text latest_visit_doc_textbox;

    //Patient Profile inputs
    private string firstname_input;
    private string lastname_input;
    private string gender_input;
    private string birthdate_input;
    private string weight_input;
    private string height_input;
    private string phone_input;
    private string latest_visit_doc_input;
    private string medications_input;
    private string conditions_input;

    public PatientDatabaseManager manager = new PatientDatabaseManager();


	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    // Empties the textboxes after inserting the data; Having issues with MaterialUI's text inputs...    
//    public void ClearForm() {
//        firstname_input.GetComponent<InputField>().text = string.Empty;
//        lastname_input.GetComponent<InputField>().text = string.Empty;
//        gender_input.GetComponent<InputField>().text = string.Empty;
//        birthdate_input.GetComponent<InputField>().text = string.Empty;
//        weight_input.GetComponent<InputField>().text = string.Empty;
//        height_input.GetComponent<InputField>().text = string.Empty;
//        phone_input.GetComponent<InputField>().text = string.Empty;
//        latest_visit_doc_input.GetComponent<InputField>().text = string.Empty;
//        medications_input.GetComponent<InputField>().text = string.Empty;
//        conditions_input.GetComponent<InputField>().text = string.Empty;
//        Debug.Log("ClearForm: " + firstname_input.GetComponent<InputField>().text);
//    }

    // Changes text on patient profile according to data from GetPatientData
    public void SetProfileData(string patient_id) {
        if (patient_id == "new") {
            patient_id = manager.MostRecentPatient();
        }
        List<string> data = manager.GetPatientData(patient_id);
        Debug.Log("SetProfileData: " + patient_id);
        // Targetting very specific objects on the profile page
        this.id_textbox.GetComponent<Text>().text = data[0];
        this.fullname_textbox.GetComponent<Text>().text = data[1] + " " + data[2];
        this.gender_textbox.GetComponent<Text>().text = "<b>Gender</b>: " + data[3];
        this.birthdate_textbox.GetComponent<Text>().text = "<b>DoB</b>: " + data[4];
        this.weight_textbox.GetComponent<Text>().text = "<b>Weight(Kg)</b>: " + data[5];
        this.height_textbox.GetComponent<Text>().text = "<b>Height(m)</b>: " + data[6];
        this.phone_textbox.GetComponent<Text>().text = "<b>Phone</b>: " + data[7];
        this.appointment_textbox.GetComponent<Text>().text = "<b>Appt</b>: " + data[8];
        this.latest_visit_date_textbox.GetComponent<Text>().text = data[9];
        this.latest_visit_doc_textbox.GetComponent<Text>().text = data[10];
    }

    public void AddPatientData() {
        Dictionary<string, string> data = new Dictionary<string, string>();
        // Targetting very specific input field objects on the form page
        // Maybe use FindGameObjectsWithTag instead to get a list
        firstname_input = GameObject.FindGameObjectWithTag("Firstname_Inputfield").GetComponent<InputField>().text.Trim();
        lastname_input = GameObject.FindGameObjectWithTag("Lastname_Inputfield").GetComponent<InputField>().text.Trim();
        gender_input = GameObject.FindGameObjectWithTag("Gender_Inputfield").GetComponent<InputField>().text.Trim();
        birthdate_input = GameObject.FindGameObjectWithTag("DoB_Inputfield").GetComponent<InputField>().text.Trim();
        weight_input = GameObject.FindGameObjectWithTag("Weight_Inputfield").GetComponent<InputField>().text.Trim();
        height_input = GameObject.FindGameObjectWithTag("Height_Inputfield").GetComponent<InputField>().text.Trim();
        phone_input = GameObject.FindGameObjectWithTag("Phone_Inputfield").GetComponent<InputField>().text.Trim();
        medications_input = GameObject.FindGameObjectWithTag("Meds_Inputfield").GetComponent<InputField>().text.Trim();
        conditions_input = GameObject.FindGameObjectWithTag("Conditions_Inputfield").GetComponent<InputField>().text.Trim();
        latest_visit_doc_input = GameObject.FindGameObjectWithTag("Doc_Inputfield").GetComponent<InputField>().text.Trim();
        data.Add("first_name", firstname_input);
        data.Add("last_name", lastname_input);
        data.Add("gender", gender_input);
//        bool malebool = this.gender_m_input.GetComponent<Toggle>().isOn;
//        bool femalebool = this.gender_f_input.GetComponent<Toggle>().isOn;
//        string gender;
//        if (malebool == true) {
//            gender = "M";
//        } else if (femalebool == true) {
//            gender = "F";
//        } else {
//            gender = "";
//        }
//        data.Add("gender", gender);
        data.Add("date_of_birth", birthdate_input);
        data.Add("weight", weight_input);
        data.Add("height", height_input);
        data.Add("phone_number", phone_input);
        data.Add("latest_visit_doctor", latest_visit_doc_input);
        data.Add("medications", medications_input);
        data.Add("medical_conditions", conditions_input);
        Debug.Log("AddPatientData: " + data["first_name"]);
        // Addresses when the user enters nothing in the form
        bool goAhead = false;
        foreach (KeyValuePair<string, string> pair in data) {
            if (!manager.ConsistsOfWhiteSpace(pair.Value)) {
                goAhead = true;
            }
        }
        if (goAhead) {
            manager.InsertPatientData("patients", data);
            Debug.Log("AddPatientData: at least one inputfield was filled out");
        }
    }
}
