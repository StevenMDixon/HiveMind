export default interface PaginatedDataResponse<t>
{
    data: t[];
    dataCount: number;
}