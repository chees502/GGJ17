﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeManager : MonoBehaviour {

	int amountMainNode;
	int amountFishNOde;
	float distToInnerBound;
	float distToOuterBound;
    
	public Vector3 motherPos;
    public GameObject rootParent;
    public GameObject fishParent;
    public AudioClip octoGatherSound;

	public IList<NodeMain> mainNodes = new List<NodeMain>();

    GameInfo gameInfo;
    // Use this for initialization
    void Start () {
        rootParent = new GameObject("rootParent");
		rootParent.transform.parent = transform.parent;
        fishParent = new GameObject("fishParent");
		fishParent.transform.parent = transform.parent;
        fishParent.AddComponent<FishManager>();

        motherPos = transform.position;
        gameInfo = GetComponent<GameInfo>();
        amountMainNode = gameInfo.amountMainNodes;
        octoGatherSound = Resources.Load("octoGather", typeof(AudioClip)) as AudioClip;

        amountFishNOde = Mathf.RoundToInt(gameInfo.amountFishToSpawn / amountMainNode)+Random.Range(-gameInfo.amountFishDelta,gameInfo.amountFishDelta);
        distToInnerBound = gameInfo.distToInnerBound;
        distToOuterBound = gameInfo.distToOuterBound;

		NodePlacement ();
	}
	
	// Update is called once per frame
	void Update () {
        motherPos = transform.position;
	}

	void NodePlacement(){
		float sliceDegree = 360.0f / amountMainNode;
		for (int i = 0; i < amountMainNode; i++) {
         //   Debug.Log("mother "+motherPos);
			Vector3 pos = Tools.PointOnCircle (motherPos, sliceDegree * i, Random.Range (distToInnerBound, distToOuterBound));
            //    Debug.Log("circ p "+pos);
            GameObject nodeGO = new GameObject("MainNode");
            nodeGO.transform.position = pos;
            Rigidbody rigid = nodeGO.AddComponent<Rigidbody>();
            rigid.isKinematic = true;
            rigid.useGravity = false;
            SphereCollider sphCol = nodeGO.AddComponent<SphereCollider>();
            sphCol.isTrigger = true;
            sphCol.radius = 10;
            
            NodeMain nodeMain = nodeGO.AddComponent<NodeMain>();
            nodeMain.Init(pos,amountFishNOde, rootParent.transform, fishParent.transform, ColorMaster.instance.getDefaultMaterial, gameInfo.materialTransitionSpeed);

            GameObject ps = Instantiate(Resources.Load("psPickup")) as GameObject; //GameObject.CreatePrimitive(PrimitiveType.Sphere);
            ps.transform.SetParent(nodeGO.transform);
            ps.transform.localPosition = Vector3.up;
            nodeMain.particle = ps.GetComponent<ParticleSystem>();

            mainNodes.Add (nodeMain);
            nodeGO.transform.SetParent(rootParent.transform);

            AudioSource octoGather = nodeGO.AddComponent<AudioSource>();
            octoGather.clip = octoGatherSound;
		}
	}

}
