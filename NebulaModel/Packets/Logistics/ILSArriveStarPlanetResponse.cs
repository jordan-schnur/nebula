﻿/*
 * see request for info
 */
namespace NebulaModel.Packets.Logistics
{
    public class ILSArriveStarPlanetResponse
    {
        public int[] StationGId { get; set; }
        
        public int[] StationMaxShips { get; set; }
        public int[] PlanetId { get; set; }
        public int Planet { get; set; }
        public int[] StorageLength { get; set; }
        public int[] StorageIdx { get; set; }
        public int[] ItemId { get; set; }
        public int[] Count { get; set; }
        public int[] LocalOrder { get; set; }
        public int[] RemoteOrder { get; set; }
        public int[] Max { get; set; }
        public int[] LocalLogic { get; set; }
        public int[] RemoteLogic { get; set; }
        public ILSArriveStarPlanetResponse() { }
        public ILSArriveStarPlanetResponse(int[] stationGId,
                                        int[] stationMaxShips,
                                        int[] planetId,
                                        int planet,
                                        int[] storageLength,
                                        int[] storageIdx,
                                        int[] itemId,
                                        int[] count,
                                        int[] localOrder,
                                        int[] remoteOrder,
                                        int[] max,
                                        int[] localLogic,
                                        int[] remoteLogic)
        {
            StationGId = stationGId;
            StationMaxShips = stationMaxShips;
            PlanetId = planetId;
            Planet = planet;
            StorageLength = storageLength;
            StorageIdx = storageIdx;
            ItemId = itemId;
            Count = count;
            LocalOrder = localOrder;
            RemoteOrder = remoteOrder;
            Max = max;
            LocalLogic = localLogic;
            RemoteLogic = remoteLogic;
        }
    }
}
