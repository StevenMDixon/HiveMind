import type { EnumOptionsObj } from '../../libraries/types';
import { type Drone, type Station, type EditDrone } from '../types';

export const getDrones = async (): Promise<Drone[]> => {
    const response = await fetch('/api/drone');

    if (response.ok) {
        const data = await response.json();
        return data;
    }

    return [];
}

export const getDrone = async (id: string): Promise<EditDrone> => {
    const response = await fetch('/api/drone/' + id);

    if (!response.ok) {
        throw new Error(`Failed to fetch drone: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();

    return {
        ...data,
        stations: data.stations.map((x: Station) => x.id)
        };
}

export const createDrone = async (drone: Drone) => {
    return await fetch('/api/drone', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ...drone }),
    });
}

export const updateDrone = async (drone: EditDrone) => {
    return await fetch(`/api/drone/`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ...drone }),
    });
}

export const deleteDrone = async (id: number) => {
    return await fetch('/api/drone/' + id , {
        method: 'Delete',
        headers: { 'Content-Type': 'application/json' },
    });
}

export const getStations = async (): Promise<Station[]> => {
    const response = await fetch('/api/stations');

    if (response.ok) {
        const data = await response.json();

        const stationOptions = data.stations.reduce((acc: EnumOptionsObj,  item: Station) => {
          acc[item.id] = item.name;
          return acc;
        }, {} as EnumOptionsObj);

        return stationOptions;
    }

    return [];
}