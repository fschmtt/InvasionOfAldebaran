﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using InvasionOfAldebaran.Helper;

namespace InvasionOfAldebaran.Models
{
	public class SpawnHandler
	{
		private readonly double _canvasWidth;
		private readonly double _canvasHeight;
		private readonly Coords _playerSpawn;
		private readonly List<Coords> _spawnPoints;
		private Random _r = new Random();

		private DateTime _lastMissile;
		private readonly List<AnimatedObject> _missiles;
		private readonly List<Question> _questions;

		public delegate void SpawnEventHandler(List<AnimatedObject> spawns);
		public event SpawnEventHandler ObjectsSpawned;

		public SpawnHandler(double canvasWidth, double canvasHeight)
		{
			_canvasWidth = canvasWidth;
			_canvasHeight = canvasHeight;
			_playerSpawn = new Coords(_canvasWidth / 2, _canvasHeight - 50);
			_spawnPoints = new List<Coords>();

			_missiles = new List<AnimatedObject>();
			_questions = this.MakeList();

			this.PopulateSpawnPoints();
		}

		private void PopulateSpawnPoints()
		{
			double gap = (_canvasWidth - 100) / 4;
			double canvasPos = 50;

			for (int i = 0; i < 4; i++)
			{
				Coords point = new Coords(canvasPos, 100);
				_spawnPoints.Add(point);
				canvasPos += gap;
			}
		}

		public List<AnimatedObject> SpawnEnemies(Question question)
		{
			var enemies = new List<AnimatedObject>();

			List<Brush> colors = question.Answers.Select(answer => answer.Color).ToList();
			List<Coords> spawns = _spawnPoints.Select(point => new Coords(point.X, point.Y)).ToList();

			for (int i = 0; i < 4; i++)
			{
				int rColor = _r.Next(0, 3 - i);
				int rSpawns = _r.Next(0, 3 - i);
				int rSpeed = _r.Next(0, 3);
				
				enemies.Add(new Enemy(colors[rColor], spawns[rSpawns], (Speed)rSpeed , RandomBool.Get()));

				colors.RemoveAt(rColor);
				spawns.RemoveAt(rSpawns);
			}
			return enemies;
		}
		
		public Player SpawnPlayer()
		{
			return new Player(Brushes.Blue, _playerSpawn);
		}

		/// <summary>
		/// Returns the next question from the questions array, null if there a no questions left.
		/// </summary>
		/// <returns></returns>
		public Question GetQuestion()
		{
			if (_questions.Count > 0)
			{
				var question = _questions.FirstOrDefault();
				_questions.Remove(question);
				return question;
			}
			else
				return null;
		}

		public void SpawnMissile(Player player)
		{
			_missiles.Clear();
			if (_lastMissile.AddSeconds(0.3) < DateTime.Now)
			{
				var missileSpawn = new Coords(player.Coords.X, player.Coords.Y);
				var missile = new Missile(Brushes.OrangeRed, missileSpawn);

				_missiles.Add(missile);
				_lastMissile = DateTime.Now;
				this.ObjectsSpawned?.Invoke(_missiles);
			}
		}

		private List<Question> MakeList()
		{
			var list = new List<Question>()
			{
				new Question("Wie viel Bits hat ein Byte?",
					new Answer("2", false),
					new Answer("4", false),
					new Answer("8", true),
					new Answer("16", false),
					Difficulty.Easy),

				new Question("Wie lang ist eine IPv4 Adresse?",
					new Answer("16 Bit", false),
					new Answer("32 Bit", true),
					new Answer("64 Bit", false),
					new Answer("128 Bit", false),
					Difficulty.Easy),

				new Question("Wie viele Sitze hat der Bundesrat?",
					new Answer("69", true),
					new Answer("72", false),
					new Answer("98", false),
					new Answer("112", false),
					Difficulty.Easy),

				new Question("Worüber kann man einen Monitor am PC anschließen?",
					new Answer("USB", false),
					new Answer("DCMI : ^)", false),
					new Answer("HSDPA", false),
					new Answer("DisplayPort", true),
					Difficulty.Easy)
			};
			return list;
		}
	}
}
