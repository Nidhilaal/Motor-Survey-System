export class ErrorMasterEntity {
    constructor(
        public errCode: string = '',
        public errType: string = '',
        public errDesc: string = '',
        public errCrBy: string = '',
        public errCrDt: Date = new Date(),
        public errUpBy: string = '',
        public errUpDt: Date = new Date()
    ) {}
}
