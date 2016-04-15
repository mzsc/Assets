using UnityEngine;
using System.Collections;
using System;

public class CubeParticle : MonoBehaviour {

    private ParticleSystem ParticleCube;

    void Awake(){
        ParticleCube = GetComponent<ParticleSystem>();
        ParticleCube.Stop();
        ParticleCube.Clear();
    }

	// Use this for initialization
//	void Start () {
////        Invoke("ParticlePlay", 5);
//        Invoke("DeleteCube", 5);
//	}
//        
//    void DeleteCube(){
//        ParticleCube.Play();
////        Mesh MeshCube = GetComponent<Mesh>();
//        Renderer RendererCube = GetComponent <Renderer>();
//        RendererCube.enabled = false;
//        Debug.LogWarning("123123");
//        StartCoroutine(Wait(()=>{
//            Destroy(gameObject);
//        },5));
// //       Destroy(gameObject);
////        Debug.LogWarning("123123");
////        Destroy(gameObject,5);
//        Debug.LogWarning("00000000");
//    }
//
//    IEnumerator Wait(Action action, float S){
//        yield return new WaitForSeconds(S);
//        action();
//    }
}
