using System.Collections.Generic;
using UnityEngine;

public class BTDefender : MonoBehaviour
{
    private AI _agent;
    private Node rootNode;


    private GameObject _friendlyFlag;
    private GameObject _friendlyBase;
    private GameObject _healthKit;
    private GameObject _powerUp;

    private string _healthKitName;
    private string _powerUpName;
    private string _friendlyFlagName;


    void Start()
    {   _agent = GetComponent<AI>();
        BuildBehaviorTree();
    }

    void Update()
    {
        rootNode.Decision();
    }

    private void BuildBehaviorTree()
    {
        DefineAllReferences();


        #region DefenderNodes

        CheckFlagLocation checkFriendlyFlagLocation = new CheckFlagLocation(_friendlyFlag, _friendlyBase);

        GoToLocation goToFriendlyFlag = new GoToLocation(_agent, _agent.agentActions, _friendlyFlag);
        GoToLocation goToFriendlyBase = new GoToLocation(_agent, _agent.agentActions, _friendlyBase);

        ChaseEnemy chaseEnemy = new ChaseEnemy(_agent.agentSenses, _agent.agentActions);

        WithinDetectionRange withinDetectionRange = new WithinDetectionRange(_agent.agentSenses);
        Attack attack = new Attack(_agent.agentActions, _agent.agentSenses);

        FleeCheck fleeCheck = new FleeCheck(_agent.agentSenses);

        Flee escape = new Flee(_agent.agentSenses, _agent.agentActions);

        GetNearestCollectable getNearestHealthKit = new GetNearestCollectable(_agent, _agent.agentSenses, _agent.agentActions, _healthKitName);

        CollectItem collectFriendlyFlag = new CollectItem(_agent.agentActions, _agent.agentSenses, _friendlyFlag);
        CollectItem collectHealthKit = new CollectItem(_agent.agentActions, _agent.agentSenses, null);

        DropItem dropFriendlyFlag = new DropItem(_agent.agentActions, _friendlyFlag);

        CheckInventory checkForPowerUp = new CheckInventory(_agent.agentInventory, _powerUpName);
        CheckInventory checkForHealthKit = new CheckInventory(_agent.agentInventory, _healthKitName);
        CheckInventory checkForFriendlyFlag = new CheckInventory(_agent.agentInventory, _friendlyFlagName);

        CheckHealthValue healthCheck = new CheckHealthValue(_agent.agentData);

        UseItem usePowerUp = new UseItem(_agent.agentActions, _powerUp);
        UseItem useHealthKit = new UseItem(_agent.agentActions, _healthKit);

        Stop stop = new Stop(_agent.agentActions);

        #endregion


        #region DefenderCompositeNodes

        Inverter doIHaveFrientlyFlag = new Inverter(checkForFriendlyFlag);


        Sequence Flee = new Sequence(new List<Node> { fleeCheck, escape });
        Sequence AttackEnemy = new Sequence(new List<Node> { chaseEnemy, attack });
        Sequence AttackPowerUp = new Sequence(new List<Node> { checkForPowerUp, usePowerUp, AttackEnemy });
        Selector AttackChoice = new Selector(new List<Node> { Flee, AttackPowerUp, AttackEnemy });


        Sequence FriendlyFlagRun = new Sequence(new List<Node> { doIHaveFrientlyFlag, checkFriendlyFlagLocation, goToFriendlyFlag, collectFriendlyFlag });
        Sequence FriendlyFlagReturn = new Sequence(new List<Node> { checkForFriendlyFlag, goToFriendlyBase, dropFriendlyFlag });

        Sequence HasHealthKit = new Sequence(new List<Node> { checkForHealthKit, useHealthKit });
        Sequence NoHealthKit = new Sequence(new List<Node> { getNearestHealthKit, collectHealthKit, useHealthKit });

        Selector HealthKitCheck = new Selector(new List<Node> { HasHealthKit, NoHealthKit });

        #endregion


        #region DefenderTopNodes

        Sequence Attack = new Sequence(new List<Node> { withinDetectionRange, AttackChoice });
        Selector RetriveFlag = new Selector(new List<Node> { FriendlyFlagRun, FriendlyFlagReturn });
        Sequence Health = new Sequence(new List<Node> { healthCheck, HealthKitCheck });
        Sequence Defend = new Sequence(new List<Node> { goToFriendlyFlag, stop });

        #endregion


        rootNode = new Selector(new List<Node> { Attack, RetriveFlag, Health, Defend });
    }

    private void DefineAllReferences()
    {
        _friendlyFlag = GameObject.Find(_agent.agentData.FriendlyFlagName);
        _friendlyBase = _agent.agentData.FriendlyBase;
        _healthKitName = Names.HealthKit;
        _powerUpName = Names.PowerUp;
        _friendlyFlagName = _agent.agentData.FriendlyFlagName;
        _healthKit = GameObject.Find(Names.HealthKit);
        _powerUp = GameObject.Find(Names.PowerUp);
    }
}
