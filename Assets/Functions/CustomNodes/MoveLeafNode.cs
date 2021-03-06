﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AITools;
using UnityEngine.AI;
using NodeEditorFramework;

[Node(false, "AITools/Move Node")]
public class MoveLeafNode : LeafNode
{

    public string moveTo;
    public new const string ID = "MoveNode";
    public override string GetID { get { return ID; } }

    public override string Title { get { return "Move Node"; } }
    public override Vector2 DefaultSize { get { return new Vector2(150, 60); } }

    public float reachTolerance = 0.8f;

    public override void OnBreak(BehaviourTreeAgent agent)
    {
        agent.GetComponent<NavMeshAgent>().SetDestination(agent.transform.position);
    }

    protected override IEnumerator process(BehaviourTreeAgent agent)
    {
        BehaviourTreeNodeState state = stateForAgent(agent);
        NavMeshAgent navAgent = agent.GetComponent<NavMeshAgent>();
        Vector3 target = agent.vector3Parameters[moveTo];

        Vector3 previousTargetPosition = new Vector3(float.PositiveInfinity, float.PositiveInfinity);
        while (Vector3.Distance(agent.transform.position,target) > reachTolerance)
        {
            if (navAgent.SetDestination(target) == false)
            {
               state.actualCondition = processCondition.Failure;
               yield break;
            }
            yield return null;
        }
        yield return null;
        state.actualCondition = processCondition.Sucess;
    }

}
