using System;
using System.Collections.Generic;
using System.Linq;

// a. Generic Repository
public class Repository<T>
{
    private List<T> items = new List<T>();

    public void Add(T item)
    {
        items.Add(item);
    }

    public List<T> GetAll()
    {
        return items;
    }

    public T? GetById(Func<T, bool> predicate)
    {
        return items.FirstOrDefault(predicate);
    }

    public bool Remove(Func<T, bool> predicate)
    {
        var item = items.FirstOrDefault(predicate);
        if (item == null) return false;
        items.Remove(item);
        return true;
    }
}

// b. Patient class
public class Patient
{
    public int Id { get; }
    public string Name { get; }
    public int Age { get; }
    public string Gender { get; }

    public Patient(int id, string name, int age, string gender)
    {
        Id = id;
        Name = name;
        Age = age;
        Gender = gender;
    }
}

// c. Prescription class
public class Prescription
{
    public int Id { get; }
    public int PatientId { get; }
    public string MedicationName { get; }
    public DateTime DateIssued { get; }

    public Prescription(int id, int patientId, string medicationName, DateTime dateIssued)
    {
        Id = id;
        PatientId = patientId;
        MedicationName = medicationName;
        DateIssued = dateIssued;
    }
}

// g. HealthSystemApp
public class HealthSystemApp
{
    private Repository<Patient> _patientRepo = new Repository<Patient>();
    private Repository<Prescription> _prescriptionRepo = new Repository<Prescription>();
    private Dictionary<int, List<Prescription>> _prescriptionMap = new Dictionary<int, List<Prescription>>();

    public void SeedData()
    {
        _patientRepo.Add(new Patient(1, "Isabella Baapogma", 25, "Female"));
        _patientRepo.Add(new Patient(2, "Kofi Agyekum", 36, "Male"));
        _patientRepo.Add(new Patient(3, "Nyameye Anabel", 10, "Female"));

        _prescriptionRepo.Add(new Prescription(1, 1, "Tylenol", DateTime.Now));
        _prescriptionRepo.Add(new Prescription(2, 1, "Vitamin C", DateTime.Now));
        _prescriptionRepo.Add(new Prescription(3, 2, "Paracetamol", DateTime.Now));
        _prescriptionRepo.Add(new Prescription(4, 3, "Metronidazole", DateTime.Now));
        _prescriptionRepo.Add(new Prescription(5, 2, "Cough Syrup", DateTime.Now));
    }

    public void BuildPrescriptionMap()
    {
        foreach (var prescription in _prescriptionRepo.GetAll())
        {
            if (!_prescriptionMap.ContainsKey(prescription.PatientId))
            {
                _prescriptionMap[prescription.PatientId] = new List<Prescription>();
            }
            _prescriptionMap[prescription.PatientId].Add(prescription);
        }
    }

    public void PrintAllPatients()
    {
        foreach (var patient in _patientRepo.GetAll())
        {
            Console.WriteLine($"ID: {patient.Id}, Name: {patient.Name}, Age: {patient.Age}, Gender: {patient.Gender}");
        }
    }

    public List<Prescription> GetPrescriptionsByPatientId(int patientId)
    {
        return _prescriptionMap.ContainsKey(patientId) ? _prescriptionMap[patientId] : new List<Prescription>();
    }

    public void PrintPrescriptionsForPatient(int id)
    {
        var prescriptions = GetPrescriptionsByPatientId(id);
        Console.WriteLine($"Prescriptions for Patient {id}:");
        foreach (var p in prescriptions)
        {
            Console.WriteLine($"  - {p.MedicationName} (issued {p.DateIssued.ToShortDateString()})");
        }
    }
}

class Program
{
    static void Main()
    {
        var app = new HealthSystemApp();
        app.SeedData();
        app.BuildPrescriptionMap();

        Console.WriteLine("All Patients:");
        app.PrintAllPatients();

        Console.WriteLine();
        app.PrintPrescriptionsForPatient(1);
    }
}