using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizer
{
    /// <summary>
    /// Manages the event details and participants.
    /// </summary>
    public class EventManager
    {
        private string title;
        private double costPerParticipant;
        private double feePerParticipant;
        private ParticipantManager participantManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventManager"/> class.
        /// </summary>
        public EventManager()
        {
            participantManager = new ParticipantManager();
        }

        /// <summary>
        /// Gets or sets the title of the event.
        /// </summary>
        public string Title
        {
            get { return title; }
            set
            {
                if (value == "" || value == null)
                {
                    title = "Untitled Event";
                }
                else
                {
                    title = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the cost per participant.
        /// </summary>
        public double CostPerParticipant
        {
            get { return costPerParticipant; }
            set { costPerParticipant = value; }
        }

        /// <summary>
        /// Gets or sets the fee per participant.
        /// </summary>
        public double FeePerParticipant
        {
            get { return feePerParticipant; }
            set { feePerParticipant = value; }
        }

        /// <summary>
        /// Gets the participant manager.
        /// </summary>
        public ParticipantManager Participants
        {
            get { return participantManager; }
        }

        /// <summary>
        /// Calculates the total cost of the event.
        /// </summary>
        /// <returns>The total cost of the event.</returns>
        public double CalculateTotalCost()
        {
            return participantManager.Count() * costPerParticipant;
        }

        /// <summary>
        /// Calculates the total fees collected.
        /// </summary>
        /// <returns>The total fees collected.</returns>
        public double CalculateTotalFees()
        {
            return participantManager.Count() * feePerParticipant;
        }

        /// <summary>
        /// Calculates the surplus or deficit of the event.
        /// </summary>
        /// <returns>The surplus or deficit of the event.</returns>
        public double CalculateSurplusDeficit()
        {
            return CalculateTotalFees() - CalculateTotalCost();
        }
    }
}
