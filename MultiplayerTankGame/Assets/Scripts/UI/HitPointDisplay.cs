using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitPointDisplay : MonoBehaviour {

    [SerializeField] private Image _healthBar;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.K))
        {
            UpdateHealthBar(100, 50);
        }

        RotateTowardsCamera(Camera.main.transform);
	}

    public void UpdateHealthBar(float maxHitPoints, float hitPointsLeft)
    {
        _healthBar.fillAmount = hitPointsLeft / maxHitPoints;
    }

    private void RotateTowardsCamera(Transform camera)
    {
        transform.LookAt(camera);
    }
}
