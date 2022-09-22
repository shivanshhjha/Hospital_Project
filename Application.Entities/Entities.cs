using Application.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Application.Entities;

public class Patient : Entity
{
    public int Patient_Id { get; set; }
    public int Address_Id { get; set; }
    public string First_Name { get; set; }
    public string Middle_Name { get; set; }
    public string Last_Name { get; set; }
    public string Mobile_No { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public string Age_Type { get; set; }
    public int TotalFee { get; set; }
    public int Doctor_Id { get; set; }
    public int IsAdmitted { get; set; }
    public Address Address { get; set; } = new Address();
}
public class Doctor : Entity
{
    public int Doctor_Id { get; set; }
    public string Name { get; set; }
    public int Fees { get; set; }
    public string Specialization { get; set; }
    public string Emp_Type { get; set; }
}

public class IPD_Patient : Entity
{
    public int Patient_Id { get; set; }
    public int Charges_Id { get; set; }
    public int Nurse { get; set; }
    public DateTime Admit_Date { get; set; }
    public DateTime Discharge_Date { get; set; }
    public string Room { get; set; }
    public int Medical_Store_Access { get; set; }
    public int Canteen_Access { get; set; }
    public int No_of_Days { get; set; }
    public int No_of_Visits { get; set; }
    public float Total_Amount { get; set; }
}

public class Charges : Entity
{
    public int Charges_Id { get; set; }
    public int Room_Charges { get; set; }
    public int Nurse_Charges { get; set; }
    public int Doctor_Charges { get; set; }
    public int Medicine_Charges { get; set; }
    public int Laundary_Charges { get; set; }
    public int Canteen_Charges { get; set; }
}

public class Address : Entity
{
    public int Address_Id { get; set; }
    public int House_No { get; set; }
    public string Society { get; set; }
    public string Area { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public DateTime DOB { get; set; }
}