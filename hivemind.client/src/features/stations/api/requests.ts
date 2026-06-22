import type { Station } from '../types'; 

export const getStation = async (id: string): Promise<Station> => {
    const response = await fetch('/api/stations/' + id);
    const data = await response.json();

    return data.station;
};

export const getStations = async (): Promise<Station[]> => {
    const response = await fetch('/api/stations');
    const data = await response.json();

    return data.stations;
};

export const updateStation = async(station: Station) => {
    return await fetch('/api/stations/' + station.stationId, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ...station }),
    });
}

export const createStation = async (station: Station) => {
    return await fetch('/api/stations', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ...station }),
    });
}

export const deleteStation = async (stationId: number) => {
    return await fetch(`/api/stations/${stationId}`, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' }
    });
}