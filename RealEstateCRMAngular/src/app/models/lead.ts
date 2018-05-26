import { User } from "./user";

export class Lead {
    LeadType : string;
    LeadName: string;
    PriorApproval: boolean;
    Min?: number;
    Max?: number;
    Bed?: number;
    Bath?: number;
    SqFootage?: number;
    Floors?: number;
    PhoneNumber: string;
    EmailAddress: string
    Address: string
    City: string;
    State: string;
    Zipcode?: number;
    UserId?: number;
    User: User;
    

}