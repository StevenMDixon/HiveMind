import type PaginatedDataResponse from '../../../utilities/PaginatedData';
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
    return await fetch('/api/stations/' + station.id, {
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


export const search = async (searchTerm: string, page: number, count: number): Promise<PaginatedDataResponse<Station>> => {
    let url = "/api/stations";

    url += `?search=${searchTerm}&count=${count}&page=${page}`;

    const response = await fetch(url);

    if (!response.ok) {
        throw new Error(`Failed to fetch stations: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return {data: data.stations, dataCount: data.dataCount} as PaginatedDataResponse<Station>;
};