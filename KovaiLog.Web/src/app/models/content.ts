import { Type } from '../models/type';

export class Content {
    ContentId: number;
    ContentTitle: string;
    ContentType: string;
    ContentDesc: string;
    CreatedOn: string;
    UpdatedOn: string;
    ContentTypes: Type[];
}
