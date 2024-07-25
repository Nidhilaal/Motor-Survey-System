export class CodesMasterEntity {
    constructor(
        public cmCode: string = '',
        public cmType: string = '',
        public cmDesc: string = '',
        public cmValue: number = 0,
        public cmCrBy: string = '',
        public cmCrDt: Date = new Date(),
        public cmUpBy: string = '',
        public cmUpDt: Date = new Date(),
        public cmActiveYn: string = ''
    ) {}
}
