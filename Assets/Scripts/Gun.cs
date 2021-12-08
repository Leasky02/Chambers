using UnityEngine.UI;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //player variable
    [SerializeField] private GameObject player;
    //contains if object is pistol or rifle
    [SerializeField] private bool isPistol;
    //variable to stop hold to shoot for pistol
    private bool canShoot = true;
    
    //defines if camera should zoom out
    public bool isZoomed = false;
    public bool zoomOut;
    //variable if aim has been released
    private bool aimReleased;
    //variable if aim has been released
    private bool aimPressed;

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
    public int totalAmmo;
    public bool reloading = false;
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
            if (Input.GetAxisRaw("Shoot") == 1 && Time.time >= nextTimeToFire)
            {
                //if gun hasnt already been shot when trigger is first pressed
                if(canShoot)
                {
                    //sets next time to fire to current time + 1 second divided by fire rate (shots per second)
                    nextTimeToFire = Time.time + 1f / fireRate;
                    //if ther is ammo and gun isnt reloading
                    if (loadedAmmo > 0 && !reloading)
                    {
                        Shoot();
                    }
                    canShoot = false;
                }
            }
        }

        //if left mouse click, then shoot (pistol)
        if (!isPistol)
        {
            //if right trigger has been pressed
            if (Input.GetAxisRaw("Shoot") == 1 && Time.time >= nextTimeToFire)
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
        //if right trigger has been pressed
        if (Input.GetAxisRaw("Shoot") == 0 && Time.time >= nextTimeToFire)
        {
            canShoot = true;
        }


        //if reload is pressed
        if (Input.GetButtonDown("Reload"))
        {
            //if there is ammo to reload with, reload
            if(totalAmmo > 0 && !reloading && loadedAmmo < maxAmmo)
                Reload();
        }

        //if aim button is pressed
        if (Input.GetAxisRaw("Aim") == 1 || Input.GetButtonDown("Aim"))
        {
            if(!aimPressed && !reloading)
            {
                //allows camera to zoom in to 40 FOV
                isZoomed = true;

                //allows camera to zoom out
                zoomOut = true;
                //play animation
                if (isPistol)
                    myAnimator.Play("AimStartPistol");
                else
                    myAnimator.Play("AimStart");

                //aim pressed is true
                aimPressed = true;
                aimReleased = false;
            }

        }

        //if aim button is released
        if (Input.GetAxisRaw("Aim") == 0 || Input.GetButtonUp("Aim"))
        {
            if(!aimReleased && !reloading)
            {
                //sets camera to zoom out to 60 FOV
                isZoomed = false;

                //zoom camera out
                if (zoomOut)
                {
                    //play animation
                    if (isPistol)
                        myAnimator.Play("AimEndPistol");
                    else
                        myAnimator.Play("AimEnd");
                }

                aimPressed = false;
                aimReleased = true;
            }
        }
        //if camera should be zoomed in
        if(isZoomed)
        {
            //then if the player is sprinting
            if (player.GetComponent<PlayerMovement>().sprinting)
            {
                //Zoom to FOV + 20
                fpsCam.fieldOfView = Mathf.Lerp(fpsCam.fieldOfView, FOV + 20, Time.deltaTime * 7f);
            }
            else
            {
                //if so, zoom to FOV
                fpsCam.fieldOfView = Mathf.Lerp(fpsCam.fieldOfView, FOV, Time.deltaTime * 7);
            }

            //slow down player speed
            player.GetComponent<PlayerMovement>().speed = 3.5f;
            //lower mouse sensetivity
            fpsCam.GetComponent<MoveLook>().mouseSensetivity = 120f;
        }
        //if camera should be zoomed out
        if(!isZoomed)
        {
            //then if player is sprinting
            if(player.GetComponent<PlayerMovement>().sprinting)
            {
                //zoom to normal + 20
                fpsCam.fieldOfView = Mathf.Lerp(fpsCam.fieldOfView, 80, Time.deltaTime * 7);
            }
            else
            {
                //if not, zoom to normal
                fpsCam.fieldOfView = Mathf.Lerp(fpsCam.fieldOfView, 60, Time.deltaTime * 7);
            }

            //speed up player speed
            player.GetComponent<PlayerMovement>().speed = 7f;
            //increase mouse sensetivity
            fpsCam.GetComponent<MoveLook>().mouseSensetivity = 250f;
        }

        //if no ammo, reload automatically
        if(loadedAmmo == 0 && totalAmmo > 0 && reloading == false)
        {
            Invoke("Reload", 0.3f);
        }

        //update ammo count
        totalAmmoDisplay.text = ("" + totalAmmo);
        loadedAmmoDisplay.text = ("" + loadedAmmo);
    }

    void Shoot()
    {
        //play muzzle flash particles
        muzzleFlash.Play();

        //if camera is zoomed in...
        if(isZoomed)
        {
            if(isPistol)
            {
                //play shooting animation
                myAnimator.Play("zoomShootPistol");
            }
            else
            {
                //play shooting animation
                myAnimator.Play("zoomShootRifle");
            }
        }
        //else (camera is not zoomed in)
        else
        {
            //play shooting animation
            myAnimator.Play("shoot");
        }
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
                Debug.Log("Hit");
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
