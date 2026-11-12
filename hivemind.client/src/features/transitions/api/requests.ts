import type PaginatedDataResponse from '../../../utilities/PaginatedData';
import type { Transition } from '../types';

const Route = '/api/transitions/';

export const getTransition = async (id: string): Promise<Transition> => {
    const response = await fetch(Route + id);
    const data = await response.json();

    return data;
};

export const getTransitions = async (): Promise<Transition[]> => {
    const response = await fetch(Route);
    const data = await response.json();

    return data.transitions;
};

export const updateTransitions = async(transition: Transition) => {
    return await fetch(Route, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ...transition }),
    });
}

export const createTransitions = async (transition: Transition) => {
    return await fetch(Route, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ...transition }),
    });
}

export const deleteTransitions = async (transitionId: number) => {
    return await fetch(Route + transitionId, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' }
    });
}

export const search = async (searchTerm: string, page: number, count: number): Promise<PaginatedDataResponse<Transition>> => {
    const params = `?search=${searchTerm}&count=${count}&page=${page}`;

    const response = await fetch(Route + params);

    if (!response.ok) {
        throw new Error(`Failed to fetch transitions: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();

    return { data: data.transitions, dataCount: data.dataCount } as PaginatedDataResponse<Transition>;
};

export const getSlotTypes = async () => {
    const response = await fetch(Route + 'slot-types', {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' }
    });

    if (!response.ok) {
        throw new Error(`Failed to query slot types: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data.slots;
}