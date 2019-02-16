﻿using Behaviour.Packages;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;

namespace Behaviour
{
	public class Simulation
	{
		public void Start()
		{
			Console.WriteLine ("Outputting Node Manifest...");
			Console.WriteLine(Manifest.NodeManifest.Construct (new Type[] { typeof(StatsNode), typeof(RollNode) }));

			//Console.WriteLine ("Outputting Type Manifest...");
			//Console.WriteLine(Manifest.TypeManifest.ConstructBaseTypes ());

			Console.WriteLine("Importing Graph...");

			var pkg = Package.Load("Content/Core");
			Console.WriteLine(pkg.Name);
			pkg.WritePackage("Content/Core.bpkg");
			
			var packageItem = JsonConvert.DeserializeObject<PackageItem>(string.Join("\n", File.ReadAllLines("Content/Core/Longsword/Main.bhvr")));
			Console.WriteLine("\tImported: " + packageItem.Name);
			var unpackedGraph = packageItem.Unpack();

			Console.WriteLine ("Running Simulation...");

			var player = new Actor();

			IBehaviour instancedItem = unpackedGraph.Setup(player);
			for (int i = 0; i < 5; i++)
			{
				Thread.Sleep(100);
				player.Health.Value -= 20;
			}
			instancedItem.Remove();

			for (int i = 0; i < 5; i++)
			{
				Thread.Sleep(100);
				player.Health.Value -= 20;
			}
		}
	}
}