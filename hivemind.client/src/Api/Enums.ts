import type { EnumOptionsObj } from "../Types/General";
import type { QuerySetting, QuerySettingItem } from "../Types/Query";
import { toEnumOptions } from "../Utilities/FormOptionsMapper"


export const fetchQueryTypes = async (): Promise<EnumOptionsObj> => {
    const response = await fetch('/queries/types');

    if (!response.ok) {
        throw new Error(`Failed to fetch query Types: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    const options = data.types as QuerySettingItem[];

    return toEnumOptions(options, 'id', 'name');
};

export const fetchPlayoutTypes = async (): Promise<EnumOptionsObj> => {
    const response = await fetch('/playout-types');

    if (!response.ok) {
        throw new Error(`Failed to fetch playout Types: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();

    const options = data.types as QuerySettingItem[];
;
    return toEnumOptions(options, 'id', 'name');
};

export const fetchPadToOptions = async (): Promise<EnumOptionsObj> => {
    const response = await fetch('/padto');

    if (!response.ok) {
        throw new Error(`Failed to get pad to options: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();

    const options = data.options as QuerySettingItem[];

    return toEnumOptions(options, 'id', 'name');
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