using Entity;
using System;
using System.Drawing;
using System.Xml.Schema;
using Unit;
using UnityEngine;

namespace State
{
    public class RunningState : StateBase
    {
        private readonly UnitBase unit;
        private readonly Vector3 point;

        private readonly float beginSpeed;
        public RunningState(UnitBase unit, Vector3 point)
        {
            this.unit = unit;
            this.point = point;

            this.beginSpeed = unit.Agent.speed;
        }

        public override void Enter()
        {
            unit.Agent.SetDestination(point);
            unit.Animator.Play(AnimationStateNames.RUNNING);
            unit.Agent.speed = 6f;
        }


        public override void Update()
        {
            Debug.Log(unit.Agent.pathStatus);

            if ((point - unit.Position).sqrMagnitude < unit.Agent.stoppingDistance)
            {
                unit.Agent.ResetPath();
                IsFinished = true;
            }
        }


        public override void Exit()
        {
            unit.Agent.speed = beginSpeed;
        }

    }


    public class MoveStateWithAction : MovableStateBase
    {
        private readonly UnitBase unit;
        private readonly Action action;
        private readonly EntityBase entity;

        public MoveStateWithAction(UnitBase unit, EntityBase entityBase, Action action)
        {
            this.unit = unit;
            this.action = action;
        }



        public override void Enter()
        {

            unit.Animator.Play(unit.RunningAnimation);
            SetDestinationAsyncRunner(unit, entity);
        }

        public override void Update()
        {

            if ((entity.Position - unit.Position).sqrMagnitude < unit.Agent.stoppingDistance)
            {
                unit.Agent.ResetPath();
                action();
            }


        }




        public override void Exit()
        {
            base.Exit();
        }

    }

}