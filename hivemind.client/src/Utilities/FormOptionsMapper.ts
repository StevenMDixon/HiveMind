//import type { EnumOptionsObj } from '../Types/General'; 

export function getProperty<Type, Key extends keyof Type>(obj: Type, key: Key | string) {
    return obj[key as Key];
}

export function getKey() { }

//export const mapObjectForOptions = <T, K extends keyof T, V extends keyof T>(objectToMap: T[], idKey: K, valKey: V): EnumOptionsObj => {

//    return objectToMap.reduce((acc, current) => {
//        const key = current[idKey];

//        if (typeof key !== "number") {
//            throw new Error("idKey must point to a number property");
//        }

//        acc[key] = current[valKey];
//        return acc;
//    }, {} as EnumOptionsObj)
//}

export const getValueFromMappedObject = <T, K extends keyof T>(object: T, key: K,) => {
        console.log(object, key)
        return object[key];
}

export function toEnumOptions<
    T,
    K extends keyof T,
    V extends keyof T
>(
    items: T[],
    keyField: K,
    valueField: V,
    includeNone?: boolean
): Record<number, string | number | boolean> {
    const results =  items.reduce((acc, current) => {
        acc[current[keyField] as unknown as number] =
            current[valueField] as unknown as string | number | boolean;

        return acc;
    }, {} as Record<number, string | number | boolean>);

    if (includeNone) results[-1] = "None";

    return results;
}