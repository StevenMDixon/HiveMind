export interface Drone {
    droneId: number;
    name: string;
    hostName: string;
    stations: Station[];
}

export interface Station {
    stationId: number;
    stationNumber: string;
    stationName: string;
    stationLogo: string;
}
