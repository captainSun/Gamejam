﻿#if (OBI_BURST && OBI_MATHEMATICS && OBI_COLLECTIONS)
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Burst;
using System;
using System.Collections;

namespace Obi
{
    [BurstCompile]
    struct PredictPositionsJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<int> activeParticles;
        [ReadOnly] public NativeArray<int> phases;
        [ReadOnly] public NativeArray<float4> buoyancies;

        // linear/position properties:
        [ReadOnly] public NativeArray<float4> externalForces;
        [ReadOnly] public NativeArray<float> inverseMasses;
        [NativeDisableParallelForRestriction] public NativeArray<float4> previousPositions;
        [NativeDisableParallelForRestriction] public NativeArray<float4> positions;
        [NativeDisableParallelForRestriction] public NativeArray<float4> velocities;

        // angular/orientation properties:
        [ReadOnly] public NativeArray<float4> externalTorques;
        [ReadOnly] public NativeArray<float> inverseRotationalMasses;
        [NativeDisableParallelForRestriction] public NativeArray<quaternion> previousOrientations;
        [NativeDisableParallelForRestriction] public NativeArray<quaternion> orientations;
        [NativeDisableParallelForRestriction] public NativeArray<float4> angularVelocities;

        [ReadOnly] public float4 gravity;
        [ReadOnly] public float deltaTime;
        [ReadOnly] public bool is2D;

        public void Execute(int index)
        {
            int i = activeParticles[index];

            // the previous position/orientation is the current position/orientation at the start of the step.
            previousPositions[i] = positions[i];
            previousOrientations[i] = orientations[i];

            if (inverseMasses[i] > 0)
            {
                float4 effectiveGravity = gravity;

                // Adjust gravity for buoyant fluid particles:
                if ((phases[i] & (int)ObiUtils.ParticleFlags.Fluid) != 0)
                    effectiveGravity *= -buoyancies[i].z;

                // apply external forces and gravity:
                float4 vel = velocities[i] + (inverseMasses[i] * externalForces[i] + effectiveGravity) * deltaTime; 

                // project velocity to 2D plane if needed:
                if (is2D)
                    vel[3] = 0;

                velocities[i] = vel;
            }

            if (inverseRotationalMasses[i] > 0)
            {
                // apply external torques (simplification: we don't use full inertia tensor here)
                float3 angularVel = angularVelocities[i].xyz + inverseRotationalMasses[i] * externalTorques[i].xyz * deltaTime;

                // project angular velocity to 2D plane normal if needed:
                if (is2D)
                    angularVel = angularVel.project(new float3(0, 0, 1));

                angularVelocities[i] = new float4(angularVel, angularVelocities[i].w);
            }

            // integrate velocities:
            positions[i]    = BurstIntegration.IntegrateLinear(positions[i], velocities[i], deltaTime);
            orientations[i] = BurstIntegration.IntegrateAngular(orientations[i], angularVelocities[i], deltaTime);

        }
    }
}
#endif