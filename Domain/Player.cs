﻿namespace Domain
{
    public class Player
    {
        public string Name { get; set; }
		public string Slug { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}
