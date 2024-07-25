export class UserMasterEntity {
    constructor(
        public userId: string = '',
        public userName: string = '',
        public userPassword: string = '',
        public userType: string = '',
        public userCrBy: string = '',
        public userCrDt: Date = new Date(),
        public userUpBy: string = '',
        public userUpDt: Date = new Date()
    ) {}
}
