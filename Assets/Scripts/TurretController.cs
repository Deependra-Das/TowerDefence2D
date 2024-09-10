using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretController : MonoBehaviour
{

    [SerializeField]
    private TowerTypes turretType;

    [SerializeField]
    private float targetingRadius = 3f;

    [SerializeField]
    private float rotationSpeed = 5f;

    [SerializeField]
    private LayerMask enemyMask;

    [SerializeField]
    private float rateOfFire = 1f;

    [SerializeField]
    private Transform turretRotationPoint;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform firingPoint;

    [SerializeField]
    private GameObject uiPanel;

    [SerializeField]
    private Button upgradeButton;

    [SerializeField]
    private Button removeButton;

    private Transform target;

    private float timeUntilFire = 0f;

    [SerializeField]
    private float freezemoveSpeed = 0.5f;

    [SerializeField]
    private float freezeDuration = 1f;

    [SerializeField]
    private int upgradeCost_Lvl1 = 100;

    [SerializeField]
    private int upgradeCost_Lvl2 = 500;

    private int upgradeLevel = 0;

    private float currentRateOfFire = 1f;
    private float currentTargetingRadius = 1f;
    private int currentUpgradeCost = 100;

    [SerializeField]
    private TextMeshProUGUI typeText;

    [SerializeField]
    private TextMeshProUGUI rateOfFireText;

    [SerializeField]
    private TextMeshProUGUI targetingRadiusText;

    [SerializeField]
    private TextMeshProUGUI upgradeButtonText;

    [SerializeField]
    private GameObject turretBarrel_lvl_1;

    [SerializeField]
    private GameObject turretBarrel_lvl_2;

    [SerializeField]
    private GameObject turretBarrel_lvl_3;

    private void Start()
    {
        currentRateOfFire = rateOfFire;
        currentTargetingRadius=targetingRadius;
        currentUpgradeCost= upgradeCost_Lvl1;

        upgradeButton.onClick.AddListener(UpgradeTurret);
        removeButton.onClick.AddListener(RemoveTurret);
    }

    private void Update()
    {
        if(turretType != TowerTypes.FREEZE)
        {
            if (target == null)
            {
                FindTarget();
                return;
            }
            RotateTowardsTarget();

            if (!CheckTargetInRadius())
            {
                target = null;
            }
            else
            {

                timeUntilFire += Time.deltaTime;

                if (timeUntilFire >= 1f / currentRateOfFire)
                {
                    ShootBullet();
                    timeUntilFire = 0;
                }
            }
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / currentRateOfFire)
            {
                FreezeEnemiesinRadius();
                timeUntilFire = 0;
            }
        }

    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, currentTargetingRadius, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private bool CheckTargetInRadius()
    {
        return Vector2.Distance(target.position, transform.position) <= currentTargetingRadius;
    }

    private void ShootBullet()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        BulletController bulletController = bulletObj.GetComponent<BulletController>();
        if(turretType==TowerTypes.BASIC)
        {
            AudioManager.Instance.PlayTurretSFX(AudioTypeList.bulletShot);
        }
        else if (turretType == TowerTypes.HEAVY)
        {
            AudioManager.Instance.PlayTurretSFX(AudioTypeList.missileShot);
        }

        bulletController.SetTarget(target);
    }

    private void FreezeEnemiesinRadius()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, currentTargetingRadius, (Vector2)transform.position, 0f, enemyMask);
        AudioManager.Instance.PlayTurretSFX(AudioTypeList.freezeShot);

        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hitObject = hits[i];

                EnemyController enemyObject = hitObject.transform.GetComponent<EnemyController>();
                enemyObject.UpdateSpeed(freezemoveSpeed);

                StartCoroutine(ResetEnemySpeed(enemyObject));
            }
        }

    }

    private IEnumerator ResetEnemySpeed(EnemyController enemyObject)
    {
        yield return new WaitForSeconds(freezeDuration);
        enemyObject.ResetSpeed();

    }

    public void OpenUIPanel()
    {
        uiPanel.SetActive(true);
        typeText.text="Type : " + turretType.ToString();

        rateOfFireText.text = "Rate Of Fire : " + currentRateOfFire.ToString();
        targetingRadiusText.text = "Targeting Radius : " + currentTargetingRadius.ToString();
        upgradeButtonText.text = "Upgrade for " + currentUpgradeCost.ToString();

    }

    public void CloseUIPanel()
    {
        uiPanel.SetActive(true);
        UIManager.Instance.SetHoveringState(false);
    }

    public void UpgradeTurret()
    {
        if(currentUpgradeCost>GameManager.Instance.currency)
        {
            return;
        }
        GameManager.Instance.SpendCurrency(currentUpgradeCost);
        upgradeLevel++;
        currentUpgradeCost = upgradeCost_Lvl2;
        switch(upgradeLevel)
        {
            case 1:
                UpgradeTurretLevel1();
                break;

            case 2:
                UpgradeTurretLevel2();
                break;

        }
    }

    private void UpgradeTurretLevel1()
    {
        AudioManager.Instance.PlaySFX(AudioTypeList.turretUpgrade);
        rateOfFire =rateOfFire/2;
        currentRateOfFire = rateOfFire;
        turretBarrel_lvl_1.SetActive(false);
        turretBarrel_lvl_2.SetActive(true);
    }

    private void UpgradeTurretLevel2()
    {
        AudioManager.Instance.PlaySFX(AudioTypeList.turretUpgrade);
        targetingRadius += 1;
        currentTargetingRadius = targetingRadius;

        if(turretType==TowerTypes.FREEZE)
        {
            freezeDuration = freezeDuration * 2;
        }

        turretBarrel_lvl_2.SetActive(false);
        turretBarrel_lvl_3.SetActive(true);
    }

    public void RemoveTurret()
    {
        int buildCost = TowerManager.Instance.GetTower(turretType).buildCost;
        AudioManager.Instance.PlaySFX(AudioTypeList.turretRemove);

        switch (upgradeLevel)
        {
            case 0:
                GameManager.Instance.AddCurrency(buildCost/2);
                break;
            case 1:
                GameManager.Instance.AddCurrency((buildCost+upgradeCost_Lvl1)/2);
                break;
            case 2:
                GameManager.Instance.AddCurrency((buildCost + upgradeCost_Lvl1+upgradeCost_Lvl2)/2);
                break;
        }
        UIManager.Instance.SetHoveringState(false);
        Destroy(gameObject);
    }

}
