using UnityEngine.UI;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //player variable
    [SerializeField] private GameObject player;
    //contains if object is pistol or rifle
    [SerializeField] private bool isPistol;
    
    //defines if camera should zoom out
    public bool isZoomed = false;
    public bool zoomOut;
    [SerializeField] private float FOV = 40f;
    //damage and range for gun
    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;
    [SerializeField] private float force = 30f;
    [SerializeField] private float fireRate = 15f;
    [SerializeField] private float nextTimeToFire = 0f;

    //ammo variables
    [SerializeField] private int maxAmmo = 30;
    [SerializeField] private int loadedAmmo;

    //audio variables
    [SerializeField] private AudioClip gunShotSound;
    [SerializeField] private AudioClip reloadSound;
    private AudioSource myAudioSource;

    //variable for thr animator
    private Animator myAnimator;
    //total ammo you can reload with
    [SerializeField] private int totalAmmo;
    [SerializeField] private bool reloading = false;
    //text box to show ammo count
    [SerializeField] private Text loadedAmmoDisplay;
    [SerializeField] private Text totalAmmoDisplay;
    //camera to send raycast from
    [SerializeField] private Camera fpsCam;
    //muzzle flash particles
    [SerializeField] private ParticleSystem muzzleFlash;
    //impact particles
    [SerializeField] private GameObject impactEffect;

    //start method
    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        myAnimator = GetComponent<Animator>();
        loadedAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        //if left mouse click, then shoot (pistol)
        if(isPistol)
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
            {
                //sets next time to fire to current time + 1 second divided by fire rate (shots per second)
                nextTimeToFire = Time.time + 1f / fireRate;
                //if ther is ammo and gun isnt reloading
                if (loadedAmmo > 0 && !reloading)
                {
                    Shoot();
                }
            }
        }

        //if left mouse click, then shoot (pistol)
        if (!isPistol)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                //sets next time to fire to current time + 1 second divided by fire rate (shots per second)
                nextTimeToFire = Time.time + 1f / fireRate;
                //if ther is ammo and gun isnt reloading
                if (loadedAmmo > 0 && !reloading)
                {
                    Shoot();
                }
            }
        }

        //if reload is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            //if there is ammo to reload with, reload
            if(totalAmmo > 0 && !reloading && loadedAmmo < maxAmmo)
                Reload();
        }

        //if right mouse button is pressed
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //slow down player speed
            player.GetComponent<PlayerMovement>().speed *= 0.5f;
            //lower mouse sensetivity
            fpsCam.GetComponent<MoveLook>().mouseSensetivity *= 0.5f;
            //allows camera to zoom in to 40 FOV
            isZoomed = true;

            //allows camera to zoom out
            zoomOut = true;
            //play animation
            if(isPistol)
                myAnimator.Play("AimStartPistol");
            else
                myAnimator.Play("AimStart");
        }

        //if right mouse button is released
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            //sets camera to zoom out to 60 FOV
            isZoomed = false;

            //zoom camera out
            if (zoomOut)
            {
                //slow down player speed
                player.GetComponent<PlayerMovement>().speed /= 0.5f;
                //lower mouse sensetivity
                fpsCam.GetComponent<MoveLook>().mouseSensetivity /= 0.5f;
                //play animation
                if (isPistol)
                    myAnimator.Play("AimEndPistol");
                else
                    myAnimator.Play("AimEnd");
            }
        }

        if(isZoomed)
            fpsCam.fieldOfView = Mathf.Lerp(fpsCam.fieldOfView, FOV, Time.deltaTime * 7);
        if(!isZoomed)
            fpsCam.fieldOfView = Mathf.Lerp(fpsCam.fieldOfView, 60, Time.deltaTime * 7);

        //if no ammo, reload automatically
        if(loadedAmmo == 0 && totalAmmo > 0 && reloading == false)
        {
            Reload();
        }

        //update ammo count
        totalAmmoDisplay.text = ("" + totalAmmo);
        loadedAmmoDisplay.text = ("" + loadedAmmo);
    }

    void Shoot()
    {
        //play muzzle flash particles
        muzzleFlash.Play();

        //play shoot sound
        myAudioSource.clip = gunShotSound;
        myAudioSource.Play();
        
        //variable to hold object raycast target finds
        RaycastHit hit;
        //if raycast target is hit
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //create target variable with raycast target
            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                //target takes damage
                target.TakeDamage(damage);
            }
            //if the hit object DOES have a rigidbody...
            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * force);
            }
            //creates iumpact particles where hit
            GameObject particles = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //destroys particles later
            Destroy(particles, 1f);
        }

        //use 1 ammo
        loadedAmmo -= 1;
    }

    void Reload()
    {
        //play reload animation
        myAnimator.Play("RifleReload");
        //if there is enough ammo for a full reload

        //make variable holding how much ammo will be added to get to max ammo
        int ammoToAdd = maxAmmo - loadedAmmo;

        if (totalAmmo >= ammoToAdd)
        {
            //set the total  ammo loaded to the max ammo
            loadedAmmo = maxAmmo;
            //take away ammo added from ammo supply
            totalAmmo -= ammoToAdd;
        }
        //if not enough ammo for a full reload
        else
        {
            //add amount of ammo remaining to loaded ammo
            loadedAmmo += totalAmmo;
            //set ammo supply to 0
            totalAmmo = 0;
        }

        //play reload sound
        myAudioSource.clip = reloadSound;
        myAudioSource.Play();

        //set reloading to true so gunfire doesnt happen while reloading
        reloading = true;
        Invoke("DeatcivateReload", 1.2f);

        isZoomed = false;
        zoomOut = false;
    }
    //set reloading to false to allow shooting again
    public void DeatcivateReload()
    {
        reloading = false;
    }
}
