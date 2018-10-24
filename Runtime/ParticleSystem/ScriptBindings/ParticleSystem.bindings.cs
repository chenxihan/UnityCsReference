// Unity C# reference source
// Copyright (c) Unity Technologies. For terms of use, see
// https://unity3d.com/legal/licenses/Unity_Reference_Only_License

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using RequiredByNativeCodeAttribute = UnityEngine.Scripting.RequiredByNativeCodeAttribute;

namespace UnityEngine
{
    [NativeHeader("ParticleSystemScriptingClasses.h")]
    [NativeHeader("Runtime/ParticleSystem/ParticleSystem.h")]
    [NativeHeader("Runtime/ParticleSystem/ParticleSystemGeometryJob.h")]
    [NativeHeader("Runtime/ParticleSystem/ScriptBindings/ParticleSystemScriptBindings.h")]
    [RequireComponent(typeof(Transform))]
    public partial class ParticleSystem : Component
    {
        // Properties
        extern public bool isPlaying
        {
            [NativeName("SyncJobs(false)->IsPlaying")] get;
        }
        extern public bool isEmitting
        {
            [NativeName("SyncJobs(false)->IsEmitting")] get;
        }
        extern public bool isStopped
        {
            [NativeName("SyncJobs(false)->IsStopped")] get;
        }
        extern public bool isPaused
        {
            [NativeName("SyncJobs(false)->IsPaused")] get;
        }
        extern public int particleCount
        {
            [NativeName("SyncJobs(false)->GetParticleCount")] get;
        }

        extern public float time
        {
            [NativeName("SyncJobs(false)->GetSecPosition")]
            get;
            [NativeName("SyncJobs(false)->SetSecPosition")]
            set;
        }

        extern public UInt32 randomSeed
        {
            [NativeName("GetRandomSeed")]
            get;
            [NativeName("SyncJobs(false)->SetRandomSeed")]
            set;
        }

        extern public bool useAutoRandomSeed
        {
            [NativeName("GetAutoRandomSeed")]
            get;
            [NativeName("SyncJobs(false)->SetAutoRandomSeed")]
            set;
        }

        extern public bool proceduralSimulationSupported
        {
            get;
        }

        // Current size/color helpers
        [FreeFunction(Name = "ParticleSystemScriptBindings::GetParticleCurrentSize", HasExplicitThis = true)]
        extern internal float GetParticleCurrentSize(ref Particle particle);

        [FreeFunction(Name = "ParticleSystemScriptBindings::GetParticleCurrentSize3D", HasExplicitThis = true)]
        extern internal Vector3 GetParticleCurrentSize3D(ref Particle particle);

        [FreeFunction(Name = "ParticleSystemScriptBindings::GetParticleCurrentColor", HasExplicitThis = true)]
        extern internal Color32 GetParticleCurrentColor(ref Particle particle);

        // Modules
        public MainModule main
        {
            get
            {
                return new MainModule(this);
            }
        }
        public EmissionModule emission
        {
            get
            {
                return new EmissionModule(this);
            }
        }
        public ShapeModule shape
        {
            get
            {
                return new ShapeModule(this);
            }
        }
        public VelocityOverLifetimeModule velocityOverLifetime
        {
            get
            {
                return new VelocityOverLifetimeModule(this);
            }
        }
        public LimitVelocityOverLifetimeModule limitVelocityOverLifetime
        {
            get
            {
                return new LimitVelocityOverLifetimeModule(this);
            }
        }
        public InheritVelocityModule inheritVelocity
        {
            get
            {
                return new InheritVelocityModule(this);
            }
        }
        public ForceOverLifetimeModule forceOverLifetime
        {
            get
            {
                return new ForceOverLifetimeModule(this);
            }
        }
        public ColorOverLifetimeModule colorOverLifetime
        {
            get
            {
                return new ColorOverLifetimeModule(this);
            }
        }
        public ColorBySpeedModule colorBySpeed
        {
            get
            {
                return new ColorBySpeedModule(this);
            }
        }
        public SizeOverLifetimeModule sizeOverLifetime
        {
            get
            {
                return new SizeOverLifetimeModule(this);
            }
        }
        public SizeBySpeedModule sizeBySpeed
        {
            get
            {
                return new SizeBySpeedModule(this);
            }
        }
        public RotationOverLifetimeModule rotationOverLifetime
        {
            get
            {
                return new RotationOverLifetimeModule(this);
            }
        }
        public RotationBySpeedModule rotationBySpeed
        {
            get
            {
                return new RotationBySpeedModule(this);
            }
        }
        public ExternalForcesModule externalForces
        {
            get
            {
                return new ExternalForcesModule(this);
            }
        }
        public NoiseModule noise
        {
            get
            {
                return new NoiseModule(this);
            }
        }
        public CollisionModule collision
        {
            get
            {
                return new CollisionModule(this);
            }
        }
        public TriggerModule trigger
        {
            get
            {
                return new TriggerModule(this);
            }
        }
        public SubEmittersModule subEmitters
        {
            get
            {
                return new SubEmittersModule(this);
            }
        }
        public TextureSheetAnimationModule textureSheetAnimation
        {
            get
            {
                return new TextureSheetAnimationModule(this);
            }
        }
        public LightsModule lights
        {
            get
            {
                return new LightsModule(this);
            }
        }
        public TrailModule trails
        {
            get
            {
                return new TrailModule(this);
            }
        }
        public CustomDataModule customData
        {
            get
            {
                return new CustomDataModule(this);
            }
        }

        // Set/get particles
        [FreeFunction(Name = "ParticleSystemScriptBindings::SetParticles", HasExplicitThis = true)]
        extern public void SetParticles([Out] Particle[] particles, int size, int offset);
        public void SetParticles([Out] Particle[] particles, int size) { SetParticles(particles, size, 0); }
        public void SetParticles([Out] Particle[] particles) { SetParticles(particles, -1); }

        [FreeFunction(Name = "ParticleSystemScriptBindings::GetParticles", HasExplicitThis = true)]
        extern public int GetParticles([NotNull][Out] Particle[] particles, int size, int offset);
        public int GetParticles([Out] Particle[] particles, int size) { return GetParticles(particles, size, 0); }
        public int GetParticles([Out] Particle[] particles) { return GetParticles(particles, -1); }

        // Set/get custom particle data
        [FreeFunction(Name = "ParticleSystemScriptBindings::SetCustomParticleData", HasExplicitThis = true)]
        extern public void SetCustomParticleData([NotNull] List<Vector4> customData, ParticleSystemCustomData streamIndex);
        [FreeFunction(Name = "ParticleSystemScriptBindings::GetCustomParticleData", HasExplicitThis = true)]
        extern public int GetCustomParticleData([NotNull] List<Vector4> customData, ParticleSystemCustomData streamIndex);

        // Playback
        [FreeFunction(Name = "ParticleSystemScriptBindings::Simulate", HasExplicitThis = true)]
        extern public void Simulate(float t, bool withChildren, bool restart, bool fixedTimeStep);
        public void Simulate(float t, bool withChildren, bool restart) { Simulate(t, withChildren, restart, true); }
        public void Simulate(float t, bool withChildren) { Simulate(t, withChildren, true); }
        public void Simulate(float t) { Simulate(t, true); }

        [FreeFunction(Name = "ParticleSystemScriptBindings::Play", HasExplicitThis = true)]
        extern public void Play(bool withChildren);
        public void Play() { Play(true);  }

        [FreeFunction(Name = "ParticleSystemScriptBindings::Pause", HasExplicitThis = true)]
        extern public void Pause(bool withChildren);
        public void Pause() { Pause(true); }

        [FreeFunction(Name = "ParticleSystemScriptBindings::Stop", HasExplicitThis = true)]
        extern public void Stop(bool withChildren, ParticleSystemStopBehavior stopBehavior);
        public void Stop(bool withChildren) { Stop(withChildren, ParticleSystemStopBehavior.StopEmitting); }
        public void Stop() { Stop(true); }

        [FreeFunction(Name = "ParticleSystemScriptBindings::Clear", HasExplicitThis = true)]
        extern public void Clear(bool withChildren);
        public void Clear() { Clear(true); }

        [FreeFunction(Name = "ParticleSystemScriptBindings::IsAlive", HasExplicitThis = true)]
        extern public bool IsAlive(bool withChildren);
        public bool IsAlive() { return IsAlive(true); }

        // Emission
        [RequiredByNativeCode]
        public void Emit(int count) { Emit_Internal(count); }
        [NativeName(Name = "SyncJobs()->Emit")]
        extern private void Emit_Internal(int count);

        [NativeName(Name = "SyncJobs()->EmitParticlesExternal")]
        extern public void Emit(EmitParams emitParams, int count);

        [NativeName(Name = "SyncJobs()->EmitParticleExternal")]
        extern private void EmitOld_Internal(ref ParticleSystem.Particle particle);

        // Fire a sub-emitter
        public void TriggerSubEmitter(int subEmitterIndex)
        {
            TriggerSubEmitter(subEmitterIndex, null);
        }

        public void TriggerSubEmitter(int subEmitterIndex, ref ParticleSystem.Particle particle)
        {
            TriggerSubEmitterForParticle(subEmitterIndex, particle);
        }

        [FreeFunction(Name = "ParticleSystemScriptBindings::TriggerSubEmitterForParticle", HasExplicitThis = true)]
        extern internal void TriggerSubEmitterForParticle(int subEmitterIndex, ParticleSystem.Particle particle);

        [FreeFunction(Name = "ParticleSystemScriptBindings::TriggerSubEmitter", HasExplicitThis = true)]
        extern public void TriggerSubEmitter(int subEmitterIndex, List<ParticleSystem.Particle> particles);

        [FreeFunction(Name = "ParticleSystemGeometryJob::ResetPreMappedBufferMemory")]
        extern public static void ResetPreMappedBufferMemory();


        [FreeFunction(Name = "ParticleSystemEditor::SetupDefaultParticleSystemType", HasExplicitThis = true)]
        extern internal void SetupDefaultType(ParticleSystemSubEmitterType type);

        [NativeProperty("GetState().localToWorld", TargetType = TargetType.Field)]
        extern internal Matrix4x4 localToWorldMatrix { get; }

        [NativeName("GetNoiseModule().GeneratePreviewTexture")]
        extern internal void GenerateNoisePreviewTexture(Texture2D dst);

        extern internal void CalculateEffectUIData(ref int particleCount, ref float fastestParticle, ref float slowestParticle);

        extern internal int GenerateRandomSeed();

        [FreeFunction(Name = "ParticleSystemScriptBindings::CalculateEffectUISubEmitterData", HasExplicitThis = true)]
        extern internal bool CalculateEffectUISubEmitterData(ref int particleCount, ref float fastestParticle, ref float slowestParticle);

        [FreeFunction(Name = "ParticleSystemScriptBindings::CheckVertexStreamsMatchShader")]
        extern internal static bool CheckVertexStreamsMatchShader(bool hasTangent, bool hasColor, int texCoordChannelCount, Material material, ref bool tangentError, ref bool colorError, ref bool uvError);

        [FreeFunction(Name = "ParticleSystemScriptBindings::GetMaxTexCoordStreams")]
        extern internal static int GetMaxTexCoordStreams();

    }

    public partial struct ParticleCollisionEvent
    {
        [FreeFunction(Name = "ParticleSystemScriptBindings::InstanceIDToColliderComponent")]
        extern static private Component InstanceIDToColliderComponent(int instanceID);
    }

    internal class ParticleSystemExtensionsImpl
    {
        [FreeFunction(Name = "ParticleSystemScriptBindings::GetSafeCollisionEventSize")]
        extern internal static int GetSafeCollisionEventSize(ParticleSystem ps);

        [FreeFunction(Name = "ParticleSystemScriptBindings::GetCollisionEventsDeprecated")]
        extern internal static int GetCollisionEventsDeprecated(ParticleSystem ps, GameObject go, [Out] ParticleCollisionEvent[] collisionEvents);

        [FreeFunction(Name = "ParticleSystemScriptBindings::GetSafeTriggerParticlesSize")]
        extern internal static int GetSafeTriggerParticlesSize(ParticleSystem ps, int type);

        [FreeFunction(Name = "ParticleSystemScriptBindings::GetCollisionEvents")]
        extern internal static int GetCollisionEvents(ParticleSystem ps, [NotNull] GameObject go, [NotNull] List<ParticleCollisionEvent> collisionEvents);

        [FreeFunction(Name = "ParticleSystemScriptBindings::GetTriggerParticles")]
        extern internal static int GetTriggerParticles(ParticleSystem ps, int type, [NotNull] List<ParticleSystem.Particle> particles);

        [FreeFunction(Name = "ParticleSystemScriptBindings::SetTriggerParticles")]
        extern internal static void SetTriggerParticles(ParticleSystem ps, int type, [NotNull] List<ParticleSystem.Particle> particles, int offset, int count);
    }

    public static partial class ParticlePhysicsExtensions
    {
        public static int GetSafeCollisionEventSize(this ParticleSystem ps)
        {
            return ParticleSystemExtensionsImpl.GetSafeCollisionEventSize(ps);
        }

        public static int GetCollisionEvents(this ParticleSystem ps, GameObject go, List<ParticleCollisionEvent> collisionEvents)
        {
            return ParticleSystemExtensionsImpl.GetCollisionEvents(ps, go, collisionEvents);
        }

        public static int GetSafeTriggerParticlesSize(this ParticleSystem ps, ParticleSystemTriggerEventType type)
        {
            return ParticleSystemExtensionsImpl.GetSafeTriggerParticlesSize(ps, (int)type);
        }

        public static int GetTriggerParticles(this ParticleSystem ps, ParticleSystemTriggerEventType type, List<ParticleSystem.Particle> particles)
        {
            return ParticleSystemExtensionsImpl.GetTriggerParticles(ps, (int)type, particles);
        }

        public static void SetTriggerParticles(this ParticleSystem ps, ParticleSystemTriggerEventType type, List<ParticleSystem.Particle> particles, int offset, int count)
        {
            if (particles == null) throw new ArgumentNullException("particles");
            if (offset >= particles.Count) throw new ArgumentOutOfRangeException("offset", "offset should be smaller than the size of the particles list.");
            if ((offset + count) >= particles.Count) throw new ArgumentOutOfRangeException("count", "offset+count should be smaller than the size of the particles list.");

            ParticleSystemExtensionsImpl.SetTriggerParticles(ps, (int)type, particles, offset, count);
        }

        public static void SetTriggerParticles(this ParticleSystem ps, ParticleSystemTriggerEventType type, List<ParticleSystem.Particle> particles)
        {
            ParticleSystemExtensionsImpl.SetTriggerParticles(ps, (int)type, particles, 0, particles.Count);
        }
    }
}
