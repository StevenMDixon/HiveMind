export interface Option {
    name: string,
    type: string,
    options: string
}

export interface Filter {
    queryFilterID: number;
    field: string;
    operator: string;
    value: string;
}

export interface Collection {
    collectionID: number;
    collectionName: string;
    filters: Filter[];
}