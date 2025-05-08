using Entity;
using Pool;
using State;
using System;
using UnityEngine;

namespace Build
{

    [SelectionBase]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(PoolObjectEntity))]
    public abstract class BuildBase : EntityBase, IVisualizable, IPooledObject
    {
        [field: SerializeField] public int Height { get; protected set; }
        [field: SerializeField] public int Width { get; protected set; }
        [field: SerializeField] public override Renderer BodyRenderer { get; protected set; }
        [SerializeField] protected PoolObjectEntity poolObjectEntity;
        public override Vector3 Position => BodyRenderer.transform.position;
        protected CapsuleCollider Collider { get; set; }
        public BuildType BuildType { get; protected set; }

        [NonSerialized] public bool alreadyInit = false;
        [SerializeField] private Renderer visual;

        public StateMachine Machine { get; protected set; } = new StateMachine();

        public override float Radius
        {
            get
            {
                if (Collider == null)
                {
                    Collider = GetComponent<CapsuleCollider>();
                }
                return Collider.radius;
            }
        }

        public IPoolObject PoolObject => poolObjectEntity;

        protected virtual void Awake()
        {
            poolObjectEntity.OnInitialize += OnInitializePoolObjectHandler;
        }

        protected virtual void Update()
        {
            Machine.Update();
        }

        protected virtual void OnEnable()
        {
            if (alreadyInit)
            {
                BuildUtility.OnBuildEnableInvoke(this); 
            }
        }

        protected virtual void OnDisable()
        {
            BuildUtility.OnBuildDisableOrDestroyInvoke(this);
        }

        protected virtual void OnDestroy()
        {
            BuildUtility.OnBuildDisableOrDestroyInvoke(this);
        }

        protected virtual void ChangeStateByBuildType(BuildType type)
        {
            BuildType = type;
        }

        public virtual void Build(BuildType type)
        {
            alreadyInit = true;
            ChangeStateByBuildType(type);
        }
        public virtual void Visualize()
        {
            BodyRenderer.gameObject.SetActive(false);
            visual.gameObject.SetActive(true);
        }

        public virtual void Unvisualize()
        {
            BodyRenderer.gameObject.SetActive(true);
            visual.gameObject.SetActive(false);
        }

        public virtual void SetColor(Color color)
        {
            visual.material.color = color;
        }

        protected virtual void OnInitializePoolObjectHandler()
        {
            
        }

    }
}