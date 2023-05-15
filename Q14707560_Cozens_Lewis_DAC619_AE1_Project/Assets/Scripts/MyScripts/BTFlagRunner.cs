using System.Collections.Generic;
using UnityEngine;

public class BTFlagRunner : MonoBehaviour
{
    private AI _agent;
    private Node rootNode;
    private bool bIsPowerUp;


    private GameObject _enemyFlag;
    private GameObject _friendlyBase;
    private GameObject _friendlyFlag;
    private GameObject _powerUp;

    private string _powerUpName;
    private string _enemyFlagName;
    private string _friendlyFlagName;


    [Header("Between 1 and 3")]
    public int powerUpChance;

    void Start()
    {
        _agent = GetComponent<AI>();
        PowerUpChoice();
        BuildBehaviorTree();
    }

    void Update()
    {
        rootNode.Decision();
    }

    private void BuildBehaviorTree()
    {
        DefineAllReferences();


        #region FlagRunnerNodes

        PowerUpCheck powerUpCheck = new PowerUpCheck(bIsPowerUp);

        CheckFlagLocation checkEnemyFlagLocation = new CheckFlagLocation(_enemyFlag, _friendlyBase);
        CheckFlagLocation checkFriendlyFlagLocation = new CheckFlagLocation(_friendlyFlag, _friendlyBase);

        GoToLocation goToEnemyFlag = new GoToLocation(_agent, _agent.agentActions, _enemyFlag);
        GoToLocation goToFriendlyFlag = new GoToLocation(_agent, _agent.agentActions, _friendlyFlag);
        GoToLocation goToFriendlyBase = new GoToLocation(_agent, _agent.agentActions, _friendlyBase);

        ChaseEnemy chaseEnemy = new ChaseEnemy(_agent.agentSenses, _agent.agentActions);

        DefendFlagRunner defendFlagRunner = new DefendFlagRunner(_agent.agentData, _agent.agentActions);

        CheckDefenders checkIfThereAreAnyDefenders = new CheckDefenders(_agent);

        CheckDistance closeToEnemyFlag = new CheckDistance(_agent, _enemyFlag);
        CheckDistance closeToFriendlyBase = new CheckDistance(_agent, _friendlyBase);

        WithinDetectionRange withinDetectionRange = new WithinDetectionRange(_agent.agentSenses);
        Attack attack = new Attack(_agent.agentActions, _agent.agentSenses);

        FleeCheck fleeCheck = new FleeCheck(_agent.agentSenses);

        Flee escape = new Flee(_agent.agentSenses, _agent.agentActions);

        GetNearestCollectable getNearestPowerUp = new GetNearestCollectable(_agent, _agent.agentSenses, _agent.agentActions, _powerUpName);

        CollectItem collectEnemyFlag = new CollectItem(_agent.agentActions, _agent.agentSenses, _enemyFlag);
        CollectItem collectFriendlyFlag = new CollectItem(_agent.agentActions, _agent.agentSenses, _friendlyFlag);
        CollectItem collectCollectable = new CollectItem(_agent.agentActions, _agent.agentSenses, null);

        DropItem dropEnemyFlag = new DropItem(_agent.agentActions, _enemyFlag);
        DropItem dropFriendlyFlag = new DropItem(_agent.agentActions, _friendlyFlag);

        UseItem usePowerUp = new UseItem(_agent.agentActions, _powerUp);

        CheckInventory checkForEnemyFlag = new CheckInventory(_agent.agentInventory, _enemyFlagName);
        CheckInventory checkForFriendlyFlag = new CheckInventory(_agent.agentInventory, _friendlyFlagName);
        CheckInventory checkForPowerUp = new CheckInventory(_agent.agentInventory, _powerUpName);

        Stop stop = new Stop(_agent.agentActions);

        PowerUpTrigger powerUpTrigger = new PowerUpTrigger(this);

        #endregion


        #region FlagRunnerCompositeNodes

        Inverter doIHaveEnemyFlag = new Inverter(checkForEnemyFlag);
        Inverter doIHaveFrientlyFlag = new Inverter(checkForFriendlyFlag);
        Inverter doIHavePowerUp = new Inverter(checkForPowerUp);

        Sequence EnemyFlagRun = new Sequence(new List<Node> { doIHaveEnemyFlag, checkEnemyFlagLocation, goToEnemyFlag, collectEnemyFlag });
        Sequence EnemyFlagReturn = new Sequence(new List<Node> { checkForEnemyFlag, goToFriendlyBase, dropEnemyFlag });

        Sequence FriendlyFlagRun = new Sequence(new List<Node> { doIHaveFrientlyFlag, checkFriendlyFlagLocation, goToFriendlyFlag, collectFriendlyFlag });
        Sequence FriendlyFlagReturn = new Sequence(new List<Node> { checkForFriendlyFlag, goToFriendlyBase, dropFriendlyFlag });

        Sequence Flee = new Sequence(new List<Node> { fleeCheck, escape });

        Sequence AttackEnemy = new Sequence(new List<Node> { chaseEnemy, attack });
        Sequence AttackPowerUp = new Sequence(new List<Node> { checkForPowerUp, usePowerUp, AttackEnemy });

        Sequence NearEnemyFlag = new Sequence(new List<Node> { closeToEnemyFlag, doIHaveEnemyFlag, goToEnemyFlag, collectEnemyFlag });
        Sequence NearBaseWithFriendlyFlag = new Sequence(new List<Node> { closeToFriendlyBase, checkForFriendlyFlag, goToFriendlyBase, dropFriendlyFlag });
        Sequence NearBaseWithEnemyFlag = new Sequence(new List<Node> { closeToFriendlyBase, checkForEnemyFlag, goToFriendlyBase, dropEnemyFlag });

        Selector AttackChoice = new Selector(new List<Node> { Flee, AttackPowerUp, AttackEnemy });

        #endregion


        #region FlagRunnerTopNodes

        Selector CloseToGoal = new Selector(new List<Node> { NearEnemyFlag, NearBaseWithFriendlyFlag, NearBaseWithEnemyFlag });
        Sequence PowerUp = new Sequence(new List<Node> { powerUpCheck, doIHavePowerUp, getNearestPowerUp, collectCollectable, powerUpTrigger });
        Sequence Attack = new Sequence(new List<Node> { withinDetectionRange, AttackChoice });
        Sequence Defender = new Sequence(new List<Node> { checkIfThereAreAnyDefenders, goToFriendlyFlag, stop });
        Selector FriendlyFlagRunner = new Selector(new List<Node> { FriendlyFlagRun, FriendlyFlagReturn });
        Sequence FlagDefender = new Sequence(new List<Node> { doIHaveEnemyFlag, defendFlagRunner });
        Selector EnemyFlagRunner = new Selector(new List<Node> { EnemyFlagRun, EnemyFlagReturn });

        #endregion


        rootNode = new Selector(new List<Node> { CloseToGoal, PowerUp, Attack, Defender, FriendlyFlagRunner, FlagDefender, EnemyFlagRunner });
    }

    private void DefineAllReferences()
    {
        bIsPowerUp = false;

        _enemyFlag = GameObject.Find(_agent.agentData.EnemyFlagName);
        _friendlyBase = _agent.agentData.FriendlyBase;
        _friendlyFlag = GameObject.Find(_agent.agentData.FriendlyFlagName);
        _powerUp = GameObject.Find(Names.PowerUp);
        _powerUpName = Names.PowerUp;
        _enemyFlagName = _agent.agentData.EnemyFlagName;
        _friendlyFlagName = _agent.agentData.FriendlyFlagName;
    }

    private void PowerUpChoice()
    {
        if (Random.Range(1, 4) == powerUpChance)
            bIsPowerUp = true;
    }

    public void PowerUpComplete()
    {
        bIsPowerUp = false;
    }
}
