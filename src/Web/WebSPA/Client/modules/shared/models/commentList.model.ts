import { IComment } from "./comment";

export interface ICommentList
{
    pageIndex: number;
    data: IComment[];
    pageSize: number;
    count: number;
}