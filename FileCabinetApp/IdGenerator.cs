﻿using System;

namespace FileCabinetApp
{
    /// <summary>
    /// Generates the following id.
    /// </summary>
    public class IdGenerator : IIdGenerator
    {
        private int currentId = 1;

        /// <summary>
        /// Generates the following id.
        /// </summary>
        /// <returns>Next id.</returns>
        public int GetNext()
        {
            return this.currentId++;
        }

        /// <summary>
        /// Sets initial id.
        /// </summary>
        /// <param name="id">Initial id.</param>
        public void SkipId(int id)
        {
            this.currentId = Math.Max(this.currentId, id + 1);
        }
    }
}