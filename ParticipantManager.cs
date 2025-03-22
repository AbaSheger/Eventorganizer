using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizer
{
    /// <summary>
    /// Manages a list of participants for an event.
    /// </summary>
    public class ParticipantManager
    {
        private List<Participant> participants;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParticipantManager"/> class.
        /// </summary>
        public ParticipantManager()
        {
            participants = new List<Participant>();
        }

        /// <summary>
        /// Adds a participant to the list.
        /// </summary>
        /// <param name="participant">The participant to add.</param>
        public void AddParticipant(Participant participant)
        {
            participants.Add(participant);
        }

        /// <summary>
        /// Removes a participant from the list by index.
        /// </summary>
        /// <param name="index">The index of the participant to remove.</param>
        public void RemoveParticipant(int index)
        {
            if (index >= 0 && index < participants.Count)
            {
                participants.RemoveAt(index);
            }
        }

        /// <summary>
        /// Edits a participant's information.
        /// </summary>
        /// <param name="index">The index of the participant to edit.</param>
        /// <param name="newParticipant">The new participant information.</param>
        public void EditParticipant(int index, Participant newParticipant)
        {
            if (index >= 0 && index < participants.Count)
            {
                participants[index] = newParticipant;
            }
        }

        /// <summary>
        /// Gets a participant at a specific index.
        /// </summary>
        /// <param name="index">The index of the participant to get.</param>
        /// <returns>The participant at the specified index, or null if the index is out of range.</returns>
        public Participant GetParticipantAt(int index)
        {
            if (index >= 0 && index < participants.Count)
            {
                return participants[index];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a list of participant information as strings.
        /// </summary>
        /// <returns>A list of participant information strings.</returns>
        public List<string> GetParticipantsInfo()
        {
            List<string> infoList = new List<string>();
            foreach (Participant participant in participants)
            {
                string participantInfo = participant.ToString();
                infoList.Add(participantInfo);
            }
            return infoList;
        }

        /// <summary>
        /// Gets the total number of participants.
        /// </summary>
        /// <returns>The total number of participants.</returns>
        public int Count()
        {
            return participants.Count;
        }
    }
}
