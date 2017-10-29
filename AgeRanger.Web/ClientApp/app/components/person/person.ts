
export interface Person {
    id : number;
    firstName : string;
    lastName: string;
    age? : number;
    ageGroup: AgeGroup;
}

export interface AgeGroup {
    id: number;
    minAge?: number;
    maxAge?: number;
    description: string;
}