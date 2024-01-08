// mko, 28.12.2023
//

export interface IDictionary<T> {
    [Key: string]: T;
}


class Dict<T> {
    GetValue: IDictionary<T> = {}
}

export function CreateDictionary<T>() : Dict<T>
{
    return new Dict<T>();
}


