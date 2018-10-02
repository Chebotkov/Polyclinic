using PolyclinicBL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PolyclinicDBManager.Models
{
    public class RoomsRegisterModel : IRoomsRegisterModel
    {
        private ICRUDMethods iCRUDMethods = new CRUDMethods();

        public RoomsRegisterModel()
        {

        }

        public RoomsRegisterModel(ICRUDMethods crudMethods)
        {
            if (crudMethods is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(crudMethods)));
            }

            iCRUDMethods = crudMethods;
        }

        public IEnumerable GetSpecializations()
        {
            return iCRUDMethods.GetSpecializationsNames();
        }

        public List<int> GetRooms(int specializationId)
        {
            List<int> rooms = new List<int>();

            using (var context = new PolyclinicDBContext())
            {
                var availableRooms = from r in context.Room.AsNoTracking()
                                     where r.SpecId == specializationId
                                     select r;

                foreach (Room room in availableRooms)
                {
                    rooms.Add(room.RoomNumber);
                }
            }

            return rooms;
        }

        public void SetRooms(List<int> rooms, int specializationId)
        {
            if (rooms is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(rooms)));
            }

            using (var context = new PolyclinicDBContext())
            {
                IQueryable<Room> query = context.Room;
                var Rooms = query.ToList();

                foreach (int roomNum in rooms)
                {
                    Room room = new Room { RoomNumber = roomNum, SpecId = specializationId };
                    context.Room.Add(room);
                }

                context.SaveChanges();
            }
        }
    }
}
