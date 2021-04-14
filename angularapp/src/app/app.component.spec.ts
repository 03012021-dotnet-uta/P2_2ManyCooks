// import { HttpClient } from '@angular/common/http';
// import { NgModule } from '@angular/core';
// import { async, TestBed } from '@angular/core/testing';
// import { Router } from '@angular/router';
// import { AppComponent } from './app.component';
// import { AuthService } from './auth.service';
// import { UserService } from './user-service';


// class MockAppComponent extends AppComponent {
//     constructor() {
//         let a: AuthService;
//         let h: HttpClient;
//         let u: UserService;
//         super(a, h, u);
//     }
// }

// describe('AppComponent', () => {
//     beforeEach(async () => {
//         await TestBed.configureTestingModule({
//             declarations: [
//                 AppComponent
//             ],
//         }).compileComponents();
//     });

//     // @NgModule({
//     //     declarations: [],
//     //     imports: [Router],
//     //     providers: [Router],
//     //     bootstrap: [AppComponent]
//     // })
//     beforeEach(async(() => {
//         TestBed.configureTestingModule({
//             imports: [Router],
//             providers: [{
//                 provide: AppComponent,
//                 useClass: MockAppComponent
//             }]
//         })
//             .compileComponents();
//     }));

//     it('should create the app', () => {
//         const fixture = TestBed.createComponent(AppComponent);
//         const app = fixture.componentInstance;
//         expect(app).toBeTruthy();
//     });

//     it(`should have as title 'angularapp'`, () => {
//         const fixture = TestBed.createComponent(AppComponent);
//         const app = fixture.componentInstance;
//         expect(app.title).toEqual('angularapp');
//     });

//     it('should render title', () => {
//         const fixture = TestBed.createComponent(AppComponent);
//         fixture.detectChanges();
//         const compiled = fixture.nativeElement;
//         expect(compiled.querySelector('.content span').textContent).toContain('angularapp app is running!');
//     });
// });
