using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour {
	public string currentOrder;
	public int customerID;
	public string sortedCurrentOrder;
	public float timePerVegetableOrder;
	public Image timeBar;
	public CustomerManager customerManager;
	public GameObject timeBarObject;
	public Text customerOrderText;
	public int waitTimeOffset;
	public ThirdPersonUserControl player1_ThirdPersonUserControl;
	public ThirdPersonUserControl player2_ThirdPersonUserControl;
	public int penalty;

	private float customerOrderTime;
	private IEnumerator timerCoroutine;
	private float customerWaitTime;

	public void ServeCustomer()
	{
		float waitTimePercentage = customerOrderTime/customerWaitTime;
		if (waitTimePercentage >= 70) {
			SpawnPowerUp ();
		}
		currentOrder = "";
		sortedCurrentOrder = "";
		StopCoroutine (timerCoroutine);
		timeBarObject.SetActive (false);
		customerOrderText.text = "";
		Invoke ("GetNextOrder", customerManager.waitTimeForNextCustomer);
	}

	public void GetCustomerWaitTime()
	{
		customerOrderText.text = currentOrder;
		List<char> orderList = new List<char> ();
		for (int i = 0; i < currentOrder.Length; i++) {
			if(currentOrder[i] != ','){
				orderList.Add(currentOrder[i]);
			}
		}
		orderList.Sort ();
		for (int i = 0; i < orderList.Count; i++) {
			sortedCurrentOrder += orderList [i];
		}
		customerWaitTime = sortedCurrentOrder.Length*timePerVegetableOrder;
		customerOrderTime = customerWaitTime;
		timerCoroutine = StartTimer ();
		StartCoroutine (timerCoroutine);
	}

	void GetNextOrder()
	{
		timeBar.fillAmount = 1;
		currentOrder = customerManager.orderDetails [customerManager.currentOrderNumber];
		customerManager.currentOrderNumber++;
		if (customerManager.currentOrderNumber > customerManager.orderDetails.Count - 1) {
			customerManager.currentOrderNumber = 0;
		}
		GetCustomerWaitTime ();
	}
	IEnumerator StartTimer()
	{
		timeBarObject.SetActive (true);
		while (customerOrderTime > 0) {
			customerOrderTime -= waitTimeOffset;
			float timeDiff = customerOrderTime/customerWaitTime;
			timeBar.fillAmount = timeDiff;
			yield return new WaitForSeconds (1f);
		}
		currentOrder = "";
		sortedCurrentOrder = "";
		timeBarObject.SetActive (false);
		customerOrderText.text = "";
		if (player1_ThirdPersonUserControl.isCustomerAngry) {
			player1_ThirdPersonUserControl.playerScore -= penalty * 2;
		} else {
			player1_ThirdPersonUserControl.playerScore -= penalty;
		}

		if (player1_ThirdPersonUserControl.isCustomerAngry) {
			player2_ThirdPersonUserControl.playerScore -= penalty*2;
		} else {
			player2_ThirdPersonUserControl.playerScore -= penalty;
		
		}

		player1_ThirdPersonUserControl.playerScoreText.text = player1_ThirdPersonUserControl.playerScore.ToString();
		player2_ThirdPersonUserControl.playerScoreText.text = player2_ThirdPersonUserControl.playerScore.ToString();

		player1_ThirdPersonUserControl.isCustomerAngry = false;
		player2_ThirdPersonUserControl.isCustomerAngry = false;

		customerManager.customerRenderers [customerID-1].material = customerManager.defaultMaterial;

		Invoke ("GetNextOrder", customerManager.waitTimeForNextCustomer);
		StopCoroutine (timerCoroutine);

	}

	void OnMouseDown()
	{
		Debug.Log("Press Customer");
		if (player2_ThirdPersonUserControl.isNearCustomer && !string.IsNullOrEmpty(player2_ThirdPersonUserControl.choppedVegetables)) {
			player2_ThirdPersonUserControl.CheckOrder (customerID-1);
		}
	}

	void SpawnPowerUp()
	{
	
	}
}
