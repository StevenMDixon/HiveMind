export interface Option {
    name: string,
    type: string,
    options: string
}

export interface Filter {
    id: number;
    field: number;
    operator: number;
    value: string;
}

export interface Query {
    id: number;
    name: string;
    type: string;
    filters: Filter[];
}

export interface QuerySetting {
    name: string;
    type: string;
    options: QuerySettingItem[];
}

export interface QuerySettingItem {
    id: number,
    name: string
}