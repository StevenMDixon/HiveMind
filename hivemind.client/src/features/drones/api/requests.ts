import { type Drone } from '../types';

export const getDrones = async (): Promise<Drone[]> => {
    const response = await fetch('/api/drone');

    if (response.ok) {
        const data = await response.json();
        return data;
    }

    return [];
}

export const createDrone = async (drone: Drone) => {
    return await fetch('/api/drone', {
        method: 'POST',
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