﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Enemy
{
    private const float CHASE_DISTANCE = 250.0f;
    private readonly float moveSpeed = 300.0f;

    private Texture2D visual;
    
    public DevMath.Vector2 Position
    {
        get; private set;
    }

    public float Rotation
    {
        get; private set;
    }

    public Enemy(DevMath.Vector2 position)
    {
        visual = Resources.Load<Texture2D>("pacman");

        Position = position;
    }

    public void Render()
    {
        GUIUtility.RotateAroundPivot(Rotation, (Position + new DevMath.Vector2(visual.width * .5f, visual.height * .5f)).ToUnity());

        GUI.color = Color.red;

        GUI.DrawTexture(new Rect(Position.x, Position.y, visual.width, visual.height), visual);

        GUI.color = Color.white;

        GUI.matrix = Matrix4x4.identity;
    }

    public void Update(Player player)
    {
        var diff = player.Position - Position;
        float distanceToPlayer = diff.Magnitude;

        if(distanceToPlayer < CHASE_DISTANCE)
        {
            var dir = diff.Normalized;
            Position += dir * moveSpeed * Time.deltaTime;

            Rotation = DevMath.DevMath.RadToDeg(DevMath.Vector2.Angle(new DevMath.Vector2(.0f, .0f), dir));
        }
    }
}
