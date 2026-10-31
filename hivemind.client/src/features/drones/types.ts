export interface EditDrone {
    id: number;
    name: string;
    hostName: string;
    stations: number[];
}

export interface Drone {
    id: number;
    name: string;
    hostName: string;
    stations:  Station[];
}

export interface Station {
    id: number;
    number: string;
    name: string;
    logo: string;
}
