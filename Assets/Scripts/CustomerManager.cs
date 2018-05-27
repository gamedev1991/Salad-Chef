using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour {
	public List<string> orderDetails;
	public int currentOrderNumber;
	public List<Customer> customerList;
	public int waitTimeForNextCustomer;
	public Material angryMaterial;
	public Material defaultMaterial;
	public SkinnedMeshRenderer[] customerRenderers;
	// Use this for initialization
	void Start () {
		SetInitalOrder ();
	}
	

	void SetInitalOrder()
	{
		for (int i = 0; i < customerList.Count; i++) {
			customerList [i].currentOrder = orderDetails [currentOrderNumber];
			customerList [i].GetCustomerWaitTime ();
			currentOrderNumber++;
		}
	}
}
