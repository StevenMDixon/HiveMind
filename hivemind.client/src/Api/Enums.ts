import type { EnumOptionsObj } from "../Types/General";
import type { QuerySetting, QuerySettingItem } from "../Types/Query";

export const fetchQueryTypes = async (): Promise<string[]> => {
    const response = await fetch('/queries/types');

    if (!response.ok) {
        throw new Error(`Failed to fetch query Types: ${response.status} ${response.statusText}`);
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

export const fetchSettings = async (): Promise<QuerySetting[]> => {
    const response = await fetch('/querysettings');

    if (!response.ok) {
        throw new Error(`Failed to query settings: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data.options;
};


export const fetchOptions = async (): Promise<QuerySettingItem[]> => {
    const response = await fetch('/queryoptions');

    if (!response.ok) {
        throw new Error(`Failed to query options: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data.options;
};