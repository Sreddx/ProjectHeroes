using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health;
    public MeleeAttack attack;
    public Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();   
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        if(other.tag == "Weapon"){
            health = health - 10;
            Debug.Log(health);
        }

        if(health <= 0){
            Destroy(gameObject);
        }
    }

    public void EnemyHealth(){
        if(attack.lightMelee == true){
            Debug.Log("Light");
            health -= attack.lightDamage;
        }else{
            health -= attack.heavyDamage;
        }

        if(health <= 0){
            Destroy(gameObject);
        }
    }

    IEnumerator blink(){
        for(int i = 0; i < 3; i++){
            gameObject.GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            gameObject.GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void GetHit(Vector3 direction){

        EnemyHealth();
        StartCoroutine(blink());
        if(attack.lightMelee == true){

            Vector3 force = direction * 5 + Vector3.up * 1;
            Debug.Log(force);
            rb.AddForce(force, ForceMode.Impulse);
            this.transform.parent = null;

        }else{
            Vector3 force = direction * 2 + Vector3.up * 5;
            Debug.Log(force);
            rb.AddForce(force, ForceMode.Impulse);
            this.transform.parent = null;
        }
        
    }
}
