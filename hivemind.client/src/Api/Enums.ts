import type { EnumOptionsObj } from "../Types/General";

export const fetchCollectionTypes = async (): Promise<string[]> => {
    const response = await fetch('/collections/types');

    if (!response.ok) {
        throw new Error(`Failed to fetch collection Types: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data.types.reduce((acc: EnumOptionsObj, current: string, index: number) => { acc[index] = current; return acc; }, {});
};

export const fetchPlayoutTypes = async (): Promise<string[]> => {
    const response = await fetch('/playout-types');

    if (!response.ok) {
        throw new Error(`Failed to fetch playout Types: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data.types.reduce((acc: EnumOptionsObj, current: string, index: number) => { acc[index] = current; return acc; }, {});
};